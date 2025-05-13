using SecclBackend.Models.SecclBackend.Models;
using System.Security.Principal;

namespace SecclBackend.Models
{
    public class ClientResponse
    {
        public List<Client> Data { get; set; } // Changed from ClientData to List<Client>
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    //public class ClientData
    //{
    //    public string Id { get; set; }
    //    public string FirmId { get; set; }
    //    public string Name { get; set; }
    //    public string FirstName { get; set; }
    //    public string Surname { get; set; }
    //    public string Language { get; set; }
    //    public string Currency { get; set; }
    //    public List<string> NodeId { get; set; }
    //    public string Status { get; set; }
    //    public string ClientType { get; set; }
    //    public List<string> ParentId { get; set; }
    //    public List<Account> Accounts { get; set; }
    //    public List<Position> Positions { get; set; }
    //    public List<Product> Products { get; set; }
    //    public decimal BookValue { get; set; }
    //    public decimal NonTransferBookValue { get; set; }
    //    public decimal TransferBookValue { get; set; }
    //    public decimal OpeningValue { get; set; }
    //    public decimal CurrentValue { get; set; }
    //    public decimal UninvestedCash { get; set; }
    //    public decimal ClosingCashValue { get; set; }
    //    public decimal Growth { get; set; }
    //    public decimal GrowthPercent { get; set; }
    //    public decimal AdjustedGrowth { get; set; }
    //    public decimal AdjustedGrowthPercent { get; set; }
    //    public decimal TransferValue { get; set; }
    //    public CgtData CgtData { get; set; }
    //    public List<string> Disclaimers { get; set; }
    //}
}