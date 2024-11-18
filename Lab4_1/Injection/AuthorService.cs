using Lab4_1.Models;
using Lab4_1.ModelsUpdate;
using Microsoft.EntityFrameworkCore;


namespace Lab4_1.Injection
{
    public interface IAuthorService
    {
        Task<Author> GetAuthorAsync(int id);
        DbSet<Author> Authors { get; }
        Task SaveChangesAsync();
        Task UpdateAuthorAsync(int id, AuthorUpdate modelUpdate);
    }

    public class AuthorService : IAuthorService
    {
        private readonly BookStoreContext _context;
 

        public AuthorService(BookStoreContext context)
        {
            _context = context;
        }
        public DbSet<Author> Authors => _context.Authors;  // Implementing Authors as DbSet<Author>

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Author> GetAuthorAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task UpdateAuthorAsync(int id, AuthorUpdate modelUpdate)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new ArgumentException("Author not found");
            }

            if (!string.IsNullOrEmpty(modelUpdate.FirstName))
            {
                author.FirstName = modelUpdate.FirstName;
            }

            if (!string.IsNullOrEmpty(modelUpdate.LastName))
            {
                author.LastName = modelUpdate.LastName;
            }

            if (modelUpdate.BirthDate.HasValue)
            {
                var birthDate = modelUpdate.BirthDate.Value;
                if (birthDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ArgumentException("Birth date cannot be in the future.");
                }
                author.BirthDate = birthDate;
            }

            await _context.SaveChangesAsync();
        }
    }

}
