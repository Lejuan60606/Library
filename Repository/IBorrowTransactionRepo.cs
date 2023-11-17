using LibraryApp.Repository.DataModel;

namespace LibraryApp.Repository
{
    public interface IBorrowTransactionRepo
    {
        Task<IList<BorrowTransaction>> GetAll();
        Task<BorrowTransaction> GetById(string id);
        Task<bool> Add(BorrowTransaction borrowTransaction);
        Task<bool> Update(BorrowTransaction borrowTransaction);
        Task<bool> Delete(string id);
    }
}
