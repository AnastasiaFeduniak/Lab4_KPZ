using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
