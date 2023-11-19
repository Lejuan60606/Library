using Repository.DataModel;

namespace Repository
{
    public interface IBorrowTransactionRepo
    {
        Task<BorrowTransaction> ReturnBook(string memberId, Book book, CancellationToken cancellationToken);
        Task<BorrowTransaction> BorrowBook(string memberId, Book book, CancellationToken cancellationToken);
        Task<List<BorrowTransaction>> GetByMemberId(string memberId, CancellationToken cancellationToken);        
    }
}
