using LibraryApp.Repository.DataModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repository
{
    public class BorrowTransactionRepo : IBorrowTransactionRepo
    {
        private readonly LibraryContext _dbContext;

        public BorrowTransactionRepo(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(BorrowTransaction BorrowTransaction, CancellationToken cancellationToken)
        {
            _dbContext.BorrowTransactions.Add(BorrowTransaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(BorrowTransaction BorrowTransaction, CancellationToken cancellationToken)
        {
            _dbContext.BorrowTransactions.Remove(BorrowTransaction);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<BorrowTransaction>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbContext.BorrowTransactions.ToListAsync(cancellationToken);
        }

        public async Task<BorrowTransaction> GetById(string id, CancellationToken cancellationToken)
        {
            return await _dbContext.BorrowTransactions.FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
        }

        public async Task Update(string id, BorrowTransaction borrowTransaction, CancellationToken cancellationToken)
        {
            var existingBorrowTransaction = await _dbContext.BorrowTransactions.FirstOrDefaultAsync(b => b.Id == borrowTransaction.Id, cancellationToken: cancellationToken);
            if (existingBorrowTransaction != null)
            {
                

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
