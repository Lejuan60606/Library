using Repository.DataModel;

namespace Repository
{
    public interface IBorrowTransactionRepo
    {
        Task<List<BorrowTransaction>> GetAll(CancellationToken cancellationToken);
        Task<BorrowTransaction> GetById(string id, CancellationToken cancellationToken);
        Task Add(BorrowTransaction borrowTransaction, CancellationToken cancellationToken);
        Task Update(string id, BorrowTransaction borrowTransaction, CancellationToken cancellationToken);
        Task Delete(BorrowTransaction borrowTransaction, CancellationToken cancellationToken);
    }
}
