using Lab4_1.Models;

namespace Lab4_1.ModelsView
{
    public class BookCreateUpdateViewModel
    {
        public string? Title { get; set; } = null!;
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? PublicationYear { get; set; }

        public int? PublisherId { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public decimal? DiscountPercentage { get; set; }

    }
}
