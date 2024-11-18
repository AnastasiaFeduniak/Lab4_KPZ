using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string? Name { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
