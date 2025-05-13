namespace SecclBackend.Models
{
    public class PortfolioSummary
    {
        public PortfolioData Data { get; set; }
    }

    public class PortfolioData
    {
        public List<AccountPortfolio> Accounts { get; set; } = new List<AccountPortfolio>(); // Default to an empty list
        public string FirmId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Language { get; set; }
        public string Currency { get; set; }
        public List<string> NodeId { get; set; }
        public string Status { get; set; }
        public string ClientType { get; set; }
        public decimal BookValue { get; set; }
        public decimal NonTransferBookValue { get; set; }
        public decimal TransferBookValue { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal UninvestedCash { get; set; }
        public decimal ClosingCashValue { get; set; }
        public decimal Growth { get; set; }
        public decimal GrowthPercent { get; set; }
        public decimal AdjustedGrowth { get; set; }
        public decimal AdjustedGrowthPercent { get; set; }
        public decimal TransferValue { get; set; }
        public CgtData CgtData { get; set; }
        public List<Disclaimer> Disclaimers { get; set; } = new List<Disclaimer>();
    }

    public class AccountPortfolio
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal BookValue { get; set; }
        public decimal Growth { get; set; }
        public decimal GrowthPercent { get; set; }
    }

    public class CgtData
    {
        public decimal? RealisedProfitLoss { get; set; }
        public decimal? UnrealisedProfitLoss { get; set; }
        public decimal? ClosingGiaStockValue { get; set; }
    }

    public class Disclaimer
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public List<string> Fields { get; set; }
        public Entity Entity { get; set; }
    }

    public class Entity
    {
        public string Type { get; set; }
        public List<string> Ids { get; set; }
    }
}