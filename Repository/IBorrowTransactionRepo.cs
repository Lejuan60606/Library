using Repository.DataModel;

namespace Repository
{
    public interface IBorrowTransactionRepo
    {
        Task<BorrowTransaction> ReturnBook(string memberId, Book book, CancellationToken cancellationToken);
        Task BorrowBook(string memberId, Book book, CancellationToken cancellationToken);
        Task<List<BorrowTransaction>> GetByMemberId(string memberId, CancellationToken cancellationToken);
        //Task<List<BorrowTransaction>> GetAll(CancellationToken cancellationToken);        
        //Task<BorrowTransaction> GetById(string id, CancellationToken cancellationToken);
        //Task Add(BorrowTransaction borrowTransaction, CancellationToken cancellationToken);
        //Task Update(string id, BorrowTransaction borrowTransaction, CancellationToken cancellationToken);
        //Task Delete(BorrowTransaction borrowTransaction, CancellationToken cancellationToken);
    }
}
