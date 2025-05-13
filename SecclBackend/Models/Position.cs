namespace SecclBackend.Models
{
    public class Position
    {
        public string PositionType { get; set; }
        public string Currency { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal Growth { get; set; }
        public decimal GrowthPercent { get; set; }
        public decimal AdjustedGrowth { get; set; }
        public decimal AdjustedGrowthPercent { get; set; }
        public decimal Allocation { get; set; }
        public string Isin { get; set; }
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public decimal Quantity { get; set; }
        public decimal BookValue { get; set; }
        public decimal CurrentPrice { get; set; }
        public string CurrentPriceDate { get; set; }
        public decimal MinimumTransferUnit { get; set; }
        public CgtData CgtData { get; set; }
    }
}