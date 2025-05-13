using Microsoft.AspNetCore.Mvc;
using SecclBackend.Services;
using SecclBackend.Models;

namespace SecclBackend.Controllers
{
    [ApiController]
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly SecclApiService _secclApiService;

        public PortfolioController(SecclApiService secclApiService)
        {
            _secclApiService = secclApiService;
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            var token = await _secclApiService.GetAccessTokenAsync();
            var clients = await _secclApiService.GetClientsAsync(token);
            return Ok(clients);
        }

        [HttpGet("summary/{clientId}")]
        public async Task<IActionResult> GetPortfolioSummary(string clientId)
        {
            var token = await _secclApiService.GetAccessTokenAsync();
            var portfolioData = await _secclApiService.FetchPortfolioData(token, clientId);

            if (portfolioData == null || portfolioData.Count == 0)
            {
                return NotFound("Portfolio data not found for the specified client.");
            }

            // Log the JSON response for debugging
            //Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(portfolioData));

            return Ok(portfolioData.First()); // Return the first portfolio if only one is expected
        }

        // New API to get aggregated total value of three portfolios
        [HttpGet("aggregated-total")]
        public async Task<IActionResult> GetAggregatedTotalValue([FromQuery] string clientIds)
        {
            // Split the clientIds string into a list if it is not null or empty
            var clientIdList = string.IsNullOrEmpty(clientIds) 
                ? new List<string>() 
                : clientIds.Split(',').ToList();

            if (clientIdList == null || clientIdList.Count == 0)
            {
                return BadRequest("Client IDs are required.");
            }

            var token = await _secclApiService.GetAccessTokenAsync();
            decimal totalValue = 0;

            foreach (var clientId in clientIdList)
            {
                var portfolios = await _secclApiService.FetchPortfolioData(token, clientId);
                totalValue += portfolios.Sum(p => p.CurrentValue);
            }

            return Ok(new { TotalValue = totalValue });
        }

        // New API to get aggregated totals by account types
        [HttpGet("aggregated-by-account-type")]
        public async Task<IActionResult> GetAggregatedByAccountType([FromQuery] string clientIds)
        {
            // Split the clientIds string into a list if it is not null or empty
            var clientIdList = string.IsNullOrEmpty(clientIds) 
                ? new List<string>() 
                : clientIds.Split(',').ToList();

            if (clientIdList == null || clientIdList.Count == 0)
            {
                return BadRequest("Client IDs are required.");
            }

            var token = await _secclApiService.GetAccessTokenAsync();
            var accountTypeTotals = new Dictionary<string, decimal>();

            foreach (var clientId in clientIdList)
            {
                var portfolios = await _secclApiService.FetchPortfolioData(token, clientId);

                foreach (var portfolio in portfolios)
                {
                    if (accountTypeTotals.ContainsKey(portfolio.AccountType))
                    {
                        accountTypeTotals[portfolio.AccountType] += portfolio.CurrentValue;
                    }
                    else
                    {
                        accountTypeTotals[portfolio.AccountType] = portfolio.CurrentValue;
                    }
                }
            }

            return Ok(accountTypeTotals);
        }
    }
}