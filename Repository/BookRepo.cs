using Repository.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BookRepo : IBookRepo
    {
        private readonly LibraryContext _dbContext;

        public BookRepo(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Book book, CancellationToken cancellationToken)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Book book, CancellationToken cancellationToken)
        {
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Book>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbContext.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book> GetById(string id, CancellationToken cancellationToken)
        {
            return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id, cancellationToken: cancellationToken);
        }

        public async Task Update(string id, Book book, CancellationToken cancellationToken)
        {
            var existingBook = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id, cancellationToken: cancellationToken);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.PublicationYear = book.PublicationYear;
                existingBook.IsAvailable = book.IsAvailable;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
