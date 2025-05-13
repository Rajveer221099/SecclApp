namespace SecclBackend.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
        public string WrapperType { get; set; }
        public string NodeId { get; set; }
        public string NodeName { get; set; }
        public string Status { get; set; }
        public bool RecurringPayment { get; set; }
        public WrapperDetail WrapperDetail { get; set; }
        public string ClientId { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal BookValue { get; set; }
        public decimal Growth { get; set; }
        public decimal GrowthPercent { get; set; }
        public decimal AdjustedGrowth { get; set; }
        public decimal AdjustedGrowthPercent { get; set; }
        public decimal ClosingCashValue { get; set; }
        public decimal UninvestedCash { get; set; }
        public decimal ClosingStockValue { get; set; }
        public decimal Allocation { get; set; }
        public CgtData CgtData { get; set; }
        public List<Disclaimer> Disclaimers { get; set; }
    }

    public class WrapperDetail
    {
        public string WrapperType { get; set; }
        public bool Discretionary { get; set; }
        public bool Advised { get; set; }
        public bool Trust { get; set; }
        public string ClientProductId { get; set; }
        public string SchemeProductId { get; set; }
        public string ProductStatus { get; set; }
        public string AssetAllocationId { get; set; }
        public string AssetAllocationName { get; set; }
    }
}