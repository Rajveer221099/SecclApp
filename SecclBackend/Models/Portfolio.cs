namespace SecclBackend.Models
{
    public class Portfolio
    {
        public string PortfolioId { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public decimal CurrentValue { get; set; } 
        public decimal BookValue { get; set; }    
        public decimal Growth { get; set; }      
        public decimal GrowthPercent { get; set; } 
    }
}
