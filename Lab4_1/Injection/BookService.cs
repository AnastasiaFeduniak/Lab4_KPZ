using Lab4_1.Models;
using Lab4_1.ModelsUpdate;
using Microsoft.EntityFrameworkCore;

namespace Lab4_1.Injection
{
    public interface IBookService
    {
        Task<Book> GetBookAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        DbSet<Book> Books { get; }
        Task SaveChangesAsync();
        Task UpdateBookAsync(int id, BookUpdate modelUpdate);
        Task DeleteBookAsync(int id);
    }

    public class BookService : IBookService
    {
        private readonly BookStoreContext _context;

        public BookService(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public DbSet<Book> Books => _context.Books;

        // Збереження змін до бази даних
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task UpdateBookAsync(int id, BookUpdate bookUpdate)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            // Partial update logic
            if (!string.IsNullOrEmpty(bookUpdate.Title))
            {
                book.Title = bookUpdate.Title;
            }
            if (bookUpdate.AuthorId.HasValue)
            {
                book.AuthorId = bookUpdate.AuthorId.Value;
            }
            if (bookUpdate.CategoryId.HasValue)
            {
                book.CategoryId = bookUpdate.CategoryId.Value;
            }
            if (bookUpdate.PublicationYear.HasValue)
            {
                if (bookUpdate.PublicationYear.Value < 1450 || bookUpdate.PublicationYear.Value > DateTime.Now.Year)
                {
                    throw new ArgumentException("Invalid publication year");
                }
                book.PublicationYear = bookUpdate.PublicationYear.Value;
            }
            if (bookUpdate.PublisherId.HasValue)
            {
                book.PublisherId = bookUpdate.PublisherId.Value;
            }
            if (!string.IsNullOrEmpty(bookUpdate.Description))
            {
                book.Description = bookUpdate.Description;
            }
            if (bookUpdate.Price.HasValue)
            {
                book.Price = bookUpdate.Price.Value;
            }
            if (bookUpdate.DiscountPercentage.HasValue)
            {
                book.DiscountPercentage = bookUpdate.DiscountPercentage.Value;
            }
      

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}