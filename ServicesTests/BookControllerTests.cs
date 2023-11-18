using Repository;
using Repository.DataModel;
using Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ServicesTests
{
    public class Tests
    {       
        private Mock<IBookRepo> _repoMock;
        private BookController target;

        [SetUp]
        public void Setup()
        {            
            _repoMock = new Mock<IBookRepo>();
            target = new BookController(_repoMock.Object);
        }

        [Test]
        public async Task GetAllBooksNoContent()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var bookList = new List<Book>();
            _repoMock.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(bookList);

            var result = await target.GetAllBooks();

            Assert.That(result, Is.TypeOf<NoContentResult>());
        }

        [Test]
        public async Task GetAllBooksWithContent()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var bookList = new List<Book> { new Book()
            {
                Id = "id1",
                Title = "ttt2",
                Author = "aaa1",
                PublicationYear = DateTime.UtcNow,
            }
        };
            _repoMock.Setup(x => x.GetAll(cancellationToken)).ReturnsAsync(bookList);

            var result = await target.GetAllBooks();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }


        [Test]
        public async Task GetAllWithException()
        {
            CancellationToken cancellationToken = new CancellationToken();
            _repoMock.Setup(x => x.GetAll(cancellationToken)).Throws(new Exception("Internal server Error."));

            var result = await target.GetAllBooks();

            Assert.Throws<Exception>(() => _repoMock.Object.GetAll(cancellationToken));
        }

        [Test]
        public async Task GetByIdWithExistingIdReturnOK()
        {
            Assert.Pass();
        }

        [Test]
        public async Task GetByIdWithNoExistingIdNotFound()
        {
            Assert.Pass();
        }

        [Test]
        public async Task GetByIdWithNoEmptyIdBadRequest()
        {
            Assert.Pass();
        }

        [Test]
        public async Task PostBookWithEmptyIdBadRequest()
        {
            Assert.Pass();
        }
    }
}