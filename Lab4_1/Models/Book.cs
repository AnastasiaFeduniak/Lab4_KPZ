using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public int PublicationYear { get; set; }

    public int PublisherId { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public decimal? FinalPrice { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<BookAmountLocation> BookAmountLocations { get; set; } = new List<BookAmountLocation>();

    public virtual ICollection<BookAmountOrder> BookAmountOrders { get; set; } = new List<BookAmountOrder>();

    public virtual Category Category { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;
}
