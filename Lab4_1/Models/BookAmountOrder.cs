using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class BookAmountOrder
{
    public int RecordId { get; set; }

    public int OrderId { get; set; }

    public int BookId { get; set; }

    public int Quantity { get; set; }

    public decimal? SoldByPrice { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
