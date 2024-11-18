using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string LocationType { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<BookAmountLocation> BookAmountLocations { get; set; } = new List<BookAmountLocation>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual WorkingHour? WorkingHour { get; set; }
}
