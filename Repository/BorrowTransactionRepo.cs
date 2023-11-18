using Repository.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Repository
{
    public class BorrowTransactionRepo : IBorrowTransactionRepo
    {
        private readonly LibraryContext _dbContext;

        public BorrowTransactionRepo(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task Add(BorrowTransaction BorrowTransaction, CancellationToken cancellationToken)
        //{
        //    _dbContext.BorrowTransactions.Add(BorrowTransaction);
        //    await _dbContext.SaveChangesAsync(cancellationToken);
        //}

        public Task BorrowBook(string memberId, Book book, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task Delete(BorrowTransaction BorrowTransaction, CancellationToken cancellationToken)
        //{
        //    _dbContext.BorrowTransactions.Remove(BorrowTransaction);
        //    await _dbContext.SaveChangesAsync(cancellationToken);
        //}

        //public async Task<List<BorrowTransaction>> GetAll(CancellationToken cancellationToken)
        //{
        //    return await _dbContext.BorrowTransactions.ToListAsync(cancellationToken);
        //}

        //public async Task<BorrowTransaction> GetById(string id, CancellationToken cancellationToken)
        //{
        //    return await _dbContext.BorrowTransactions.FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
        //}

        public Task<List<BorrowTransaction>> GetByMemberId(string memberId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
     

        Task<BorrowTransaction> IBorrowTransactionRepo.ReturnBook(string memberId, Book book, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task Update(string id, BorrowTransaction borrowTransaction, CancellationToken cancellationToken)
        //{
        //    var existingBorrowTransaction = await _dbContext.BorrowTransactions.FirstOrDefaultAsync(b => b.Id == borrowTransaction.Id, cancellationToken: cancellationToken);
        //    if (existingBorrowTransaction != null)
        //    {
        //        existingBorrowTransaction.BookID = borrowTransaction.BookID;
        //        existingBorrowTransaction.MemberID = borrowTransaction.MemberID;
        //        existingBorrowTransaction.BorrowDate = borrowTransaction.BorrowDate;
        //        existingBorrowTransaction.ReturnDate = borrowTransaction.ReturnDate;

        //        await _dbContext.SaveChangesAsync(cancellationToken);
        //    }
        //}
    }
}
