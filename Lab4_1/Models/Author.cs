using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
