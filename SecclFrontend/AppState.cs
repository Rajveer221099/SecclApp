using System.Collections.Generic;
using SecclShared.Models;

public class AppState
{
    public List<Client> Clients { get; set; } = new();
    public Dictionary<string, decimal> AggregatedTotals { get; set; } = new();
    public decimal TotalValue { get; set; }
    public bool IsLoading { get; set; }
    public bool IsLoadingClients { get; set; }
    public bool IsClientListVisible { get; set; }
}