using LibraryApp.Repository;
using LibraryApp.Repository.DataModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace RepositoryTest
{
    [TestFixture]
    public class BorrowTransactionTests
    {
        private Mock<LibraryContext> _contextMock;
        private Mock<DbSet<BorrowTransaction>> _dbSetMock;
        private BorrowTransactionRepo _repository;

        [SetUp]
        public void Setup()
        {
            string connectionString = "string";           
            _contextMock = new Mock<LibraryContext>(connectionString);
            IEnumerable<BorrowTransaction> testData = CreateDataForTest();
            _dbSetMock = new Mock<DbSet<BorrowTransaction>>();
            _contextMock.Setup(m => m.BorrowTransactions).Returns(_dbSetMock.Object);
            _repository = new BorrowTransactionRepo(_contextMock.Object);
        }

        [Test]
        public async Task AddBorrowTransactionAsync_AddsBorrowTransaction()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var BorrowTransactions = new List<BorrowTransaction>();

            var BorrowTransaction = new BorrowTransaction()
            {
                
            };

            _dbSetMock.Setup(m => m.Add(It.IsAny<BorrowTransaction>())).Callback<BorrowTransaction>(BorrowTransactions.Add);
            _contextMock.Setup(m => m.BorrowTransactions).Returns(_dbSetMock.Object);

            await _repository.Add(BorrowTransaction, cancellationToken);

            _dbSetMock.Verify(x => x.Add(It.Is<BorrowTransaction>(a => a.Id == "id1")), Times.Exactly(1));          
            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllBorrowTransactions()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var target = new BorrowTransactionRepo(_contextMock.Object);
            var result = await target.GetAll(cancellationToken);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateBorrowTransaction()
        {           
         }

        [Test]
        public async Task GetBorrowTransactionByIdWithExistingId()
        {         
        }

        [Test]
        public async Task GetBorrowTransactionByIdWithNotExistingId()
        {           
        }

        [Test]
        public async Task DeleteBorrowTransactionWithExistingId()
        {
        }
      
        private List<BorrowTransaction> CreateDataForTest()
        {
            var transaction = new BorrowTransaction()
            {
                //Id = "id1",
                //BorrowTransactionID = "b1",
                //MemberID = "m1",
                //BorrowDate = DateTime.UtcNow,
            };

            return new List<BorrowTransaction>() { transaction };
        }      
    }

}