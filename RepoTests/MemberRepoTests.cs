using LibraryApp.Repository;
using LibraryApp.Repository.DataModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using UtilitiesForTestEFCore;

namespace RepositoryTest
{
    [TestFixture]
    public class MemberRepositoryTests
    {
        private Mock<DbSet<Member>> _dbSetMock;
        private Mock<LibraryContext> _contextMock;

        [SetUp]
        public void Setup()
        {
            string connectionString = "string";
            _contextMock = new Mock<LibraryContext>(connectionString);
            IEnumerable<Member> testData = CreateDataForTest();
            _dbSetMock = new Mock<DbSet<Member>>().SetData(testData);
            _dbSetMock = new Mock<DbSet<Member>>(); ;
            _contextMock.Setup(m => m.Members).Returns(_dbSetMock.Object);
        }

        [Test]
        public async Task AddMember()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var members = new List<Member>();

            var member = new Member()
            {
                Id = "id1",
                Name = "t1",
                JoinedDate = DateTime.UtcNow,
            };

            _dbSetMock.Setup(m => m.Add(It.IsAny<Member>())).Callback<Member>(members.Add);
            _contextMock.Setup(m => m.Members).Returns(_dbSetMock.Object);

            var target = new MemberRepo(_contextMock.Object);
            await target.Add(member, cancellationToken);


            _dbSetMock.Verify(x => x.Add(It.Is<Member>(a => a.Id == "id1")), Times.Exactly(1));
            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllBooks()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var target = new MemberRepo(_contextMock.Object);
            var result = await target.GetAll(cancellationToken);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateBook()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var member = new Member()
            {
                Id = "id1",
                Name = "ttt2",
                JoinedDate = DateTime.UtcNow,
            };
            var target = new MemberRepo(_contextMock.Object);
            await target.Update(member.Id, member, cancellationToken);

            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetBookByIdWithExistingId()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string bookId = "id1";
            var target = new MemberRepo(_contextMock.Object);
            var result = await target.GetById(bookId, cancellationToken);

            Assert.That(result.Name, Is.EqualTo("t2"));

        }

        [Test]
        public async Task GetBookByIdWithNotExistingId()
        {
            CancellationToken cancellationToken = new CancellationToken();
            string bookId = "id1";
            var target = new MemberRepo(_contextMock.Object);
            var result = await target.GetById(bookId, cancellationToken);
            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteBookWithExistingId()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var member = new Member()
            {
                Id = "id1",
                Name = "ttt2",
                JoinedDate = DateTime.UtcNow,
            };

            var target = new MemberRepo(_contextMock.Object);
            await target.Delete(member, cancellationToken);
            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        public IList<Member> CreateDataForTest()
        {
            var member = new Member()
            {
                Id = "id1",
                Name = "ttt2",               
                JoinedDate = DateTime.UtcNow,
            };

            return new List<Member> { member };
        }
    }

}