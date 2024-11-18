using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class BookAmountLocation
{
    public int RecordId { get; set; }

    public int LocationId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;
}
