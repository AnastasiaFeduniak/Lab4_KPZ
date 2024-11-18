using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class ClientOrderAnalytics1
{
    public int ClientId { get; set; }

    public string ClientFullName { get; set; } = null!;

    public int LocationId { get; set; }

    public string LocationAddress { get; set; } = null!;

    public int? TotalOrders { get; set; }

    public decimal? TotalRevenue { get; set; }

    public decimal? AvgOrderValue { get; set; }

    public long? ClientRank { get; set; }
}
