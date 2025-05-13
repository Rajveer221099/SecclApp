using System.Text.Json;
using System.Net.Http.Headers;
using SecclBackend.Models;
using SecclBackend.Models.SecclBackend.Models;

namespace SecclBackend.Services
{
    public class SecclApiService
    {
        private readonly HttpClient _httpClient;

        public SecclApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to get the API token
        public async Task<string> GetAccessTokenAsync()
        {
            var requestBody = new
            {
                firmId = "P1IMX",
                id = "nelahi6642@4tmail.net",
                password = "DemoBDM1"
            };

            var response = await _httpClient.PostAsJsonAsync("https://pfolio-api-staging.seccl.tech/authenticate", requestBody);

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

            return tokenElement.GetString();
        }

        // Method to fetch the list of clients
        public async Task<List<Client>> GetClientsAsync(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _httpClient.GetAsync("https://pfolio-api-staging.seccl.tech/client/P1IMX?pageSize=100&page=1");

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

                if (clientsResponse?.Data == null)
                {
                    Console.WriteLine("No clients found in the response.");
                    return new List<Client>();
                }

                return clientsResponse.Data;
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

            var response = await _httpClient.GetAsync($"https://pfolio-api-staging.seccl.tech/portfolio/summary/P1IMX/{clientId}");
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