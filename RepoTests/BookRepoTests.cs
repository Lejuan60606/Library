using Repository;
using Repository.DataModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using UtilitiesForTestEFCore;

namespace RepositoryTest
{
    [TestFixture]
    public class BookRepositoryTests
    {
        private Mock<DbSet<Book>> _dbSetMock;
        private Mock<LibraryContext> _contextMock;     

        [SetUp]
        public void Setup()
        {
            string connectionString = "string";
            _contextMock = new Mock<LibraryContext>(connectionString);
            IEnumerable<Book> testData = CreateDataForTest();
            _dbSetMock = new Mock<DbSet<Book>>().SetData(testData);
            _dbSetMock = new Mock<DbSet<Book>>(); ;
            _contextMock.Setup(m => m.Books).Returns(_dbSetMock.Object);
        }

        [Test]
        public async Task AddsBook()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var books = new List<Book>();

            var book = new Book()
            {
                Id = "id1",
                Title = "t1",
                Author = "a1",
                PublicationYear = DateTime.UtcNow,
            };

            _dbSetMock.Setup(m => m.Add(It.IsAny<Book>())).Callback<Book>(books.Add);
            _contextMock.Setup(m => m.Books).Returns(_dbSetMock.Object);

            var target = new BookRepo(_contextMock.Object);
            await target.Add(book, cancellationToken);


            _dbSetMock.Verify(x => x.Add(It.Is<Book>(a => a.Id == "id1")), Times.Exactly(1));
            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllBooks()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var target = new BookRepo(_contextMock.Object);
            var result = await target.GetAll(cancellationToken);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateBook()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var book = new Book()
            {
                Id = "id1",
                Title = "ttt2",
                Author = "aaa1",
                PublicationYear = DateTime.UtcNow,
            };
            var target = new BookRepo(_contextMock.Object);
            await target.Update(book.Id, book, cancellationToken);

            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetBookByIdWithExistingId()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string bookId = "id1";
            var target = new BookRepo(_contextMock.Object);
            var result = await target.GetById(bookId, cancellationToken);

            Assert.That(result.Title, Is.EqualTo("t2"));
            Assert.That(result.Author, Is.EqualTo("a1"));

        }

        [Test]
        public async Task GetBookByIdWithNotExistingId()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string bookId = "id1";
            var target = new BookRepo(_contextMock.Object);
            var result = await target.GetById(bookId, cancellationToken);
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteBookWithExistingId()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var book = new Book()
            {
                Id = "id1",
                Title = "ttt2",
                Author = "aaa1",
                PublicationYear = DateTime.UtcNow,
            };

            var target = new BookRepo(_contextMock.Object);
            await target.Delete(book, cancellationToken);
            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }      
        
        public IList<Book> CreateDataForTest()
        {
            var book = new Book()
            {
                Id = "id1",
                Title = "ttt2",
                Author = "aaa1",
                PublicationYear = DateTime.UtcNow,
            };

            return new List<Book> { book };
        }
    }

}