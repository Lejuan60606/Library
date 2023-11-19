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

        public async Task<BorrowTransaction> BorrowBook(string memberId, Book book, CancellationToken cancellationToken)
        {
            var transaction = new BorrowTransaction
            {
                Id = Guid.NewGuid().ToString(),
                BookID = book.Id,
                MemberID = memberId,
                BorrowDate = DateTime.UtcNow
            };

            _dbContext.BorrowTransactions.Add(transaction);

            // Update the availability of the book, if needed
            var bookToUpdate = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id, cancellationToken);
            if (bookToUpdate != null)
            {
                bookToUpdate.IsAvailable = false;
                _dbContext.Books.Update(bookToUpdate);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return transaction;
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

        public async Task<List<BorrowTransaction>> GetByMemberId(string memberId, CancellationToken cancellationToken)
        {         
            return await _dbContext.BorrowTransactions
                         .Where(t => t.MemberID == memberId)
                         .ToListAsync(cancellationToken);
        }
     

        public async Task<BorrowTransaction> ReturnBook(string memberId, Book book, CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.BorrowTransactions
                                   .FirstOrDefaultAsync(t => t.MemberID == memberId && t.BookID == book.Id && t.ReturnDate == null, cancellationToken);

            if (transaction != null)
            {
                transaction.ReturnDate = DateTime.UtcNow;
                _dbContext.BorrowTransactions.Update(transaction);

                // Update the availability of the book
                var bookToUpdate = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == book.Id, cancellationToken);
                if (bookToUpdate != null)
                {
                    bookToUpdate.IsAvailable = true;
                    _dbContext.Books.Update(bookToUpdate);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return transaction;
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
