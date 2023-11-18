using Repository.DataModel;

namespace Repository
{
    public interface IBookRepo
    {
        Task<List<Book>> GetAll(CancellationToken cancellationToken);
        Task<Book> GetById(string id, CancellationToken cancellationToken);
        Task Add(Book book, CancellationToken cancellationToken);
        Task Update(string id, Book book, CancellationToken cancellationToken);
        Task Delete(Book book, CancellationToken cancellationToken);
    }
}
