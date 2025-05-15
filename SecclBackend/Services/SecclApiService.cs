using System.Text.Json;
using System.Net.Http.Headers;
using SecclShared.Models;
using SecclBackend.Models;
using Microsoft.Extensions.Caching.Memory;

namespace SecclBackend.Services
{
    public class SecclApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private string? _cachedToken;
        private DateTime _tokenExpiry;

        public SecclApiService(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cache = cache;
        }

        // Method to get the API token
        public async Task<string> GetAccessTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _tokenExpiry)
            {
                return _cachedToken;
            }

            var requestBody = new
            {
                firmId = _configuration["SecclApi:FirmId"],
                id = _configuration["SecclApi:Username"],
                password = _configuration["SecclApi:Password"]
            };

            var response = await _httpClient.PostAsJsonAsync(_configuration["SecclApi:BaseUrl"] + "/authenticate", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to authenticate. Status Code: {response.StatusCode}, Response: {errorResponse}");
            }

            var responseData = await response.Content.ReadFromJsonAsync<JsonElement>();

            // Access the "token" property inside the "data" object
            if (!responseData.TryGetProperty("data", out var dataElement) ||
                !dataElement.TryGetProperty("token", out var tokenElement))
            {
                throw new KeyNotFoundException("The 'token' property was not found in the API response.");
            }

            _cachedToken = tokenElement.GetString();
            _tokenExpiry = DateTime.UtcNow.AddMinutes(25); // Set token expiry (adjust as needed)

            return _cachedToken;
        }

        // Method to fetch the list of clients
        public async Task<List<Client>> GetClientsAsync(string accessToken)
        {
            const string cacheKey = "SecclClients";
            if (_cache.TryGetValue(cacheKey, out List<Client> cachedClients))
            {
                return cachedClients;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _httpClient.GetAsync(_configuration["SecclApi:BaseUrl"] + "/client/" + _configuration["SecclApi:FirmId"] + "?pageSize=100&page=1");

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to fetch clients. Status Code: {response.StatusCode}, Response: {errorResponse}");
                }

                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Raw Client Response: {responseData}"); // Log the raw response for debugging

                var clientsResponse = JsonSerializer.Deserialize<ClientResponse>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ensure case-insensitive property matching
                });

                var clients = clientsResponse?.Data ?? new List<Client>();

                // Cache for 10 minutes
                _cache.Set(cacheKey, clients, TimeSpan.FromMinutes(10));

                return clients;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                throw new Exception("Failed to deserialize the client response.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching clients: {ex.Message}");
                throw;
            }
        }

        // Method to fetch portfolio data for a specific client
        public async Task<List<Portfolio>> FetchPortfolioData(string accessToken, string clientId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync($"{_configuration["SecclApi:BaseUrl"]}/portfolio/summary/{_configuration["SecclApi:FirmId"]}/{clientId}");
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw Portfolio Response: {responseData}"); // Log the raw response for debugging

            var portfolioSummary = JsonSerializer.Deserialize<PortfolioSummary>(responseData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ensure case-insensitive property matching
            });

            if (portfolioSummary?.Data?.Accounts == null || !portfolioSummary.Data.Accounts.Any())
            {
                Console.WriteLine("No accounts found in the portfolio data.");
                return new List<Portfolio>(); // Return an empty list if no accounts are found
            }

            // Map the accounts to the Portfolio model
            return portfolioSummary.Data.Accounts.Select(account => new Portfolio
            {
                PortfolioId = account.Id,
                Name = account.Name,
                AccountType = account.AccountType,
                Currency = account.Currency,
                CurrentValue = account.CurrentValue,
                BookValue = account.BookValue,
                Growth = account.Growth,
                GrowthPercent = account.GrowthPercent
            }).ToList();
        }
    }
}