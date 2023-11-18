using LibraryApp.Repository;
using LibraryApp.Repository.DataModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace RepositoryTest
{
    [TestFixture]
    public class MemberRepositoryTests
    {
        private Mock<LibraryContext> _contextMock;
        private Mock<DbSet<Member>> _dbSetMock;
        private MemberRepo _repository;

        [SetUp]
        public void Setup()
        {
            string connectionString = "string";           
            _contextMock = new Mock<LibraryContext>(connectionString);
            IEnumerable<Member> testData = CreateDataForTest();
            _dbSetMock = new Mock<DbSet<Member>>();
            _contextMock.Setup(m => m.Members).Returns(_dbSetMock.Object);
            _repository = new MemberRepo(_contextMock.Object);
        }

        [Test]
        public async Task AddMemberAsync_AddsMember()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var Members = new List<Member>();

            var Member = new Member()
            {
                //Id = "id1",
                //Title = "t1",
                //Author = "a1",
                //PublicationYear = DateTime.UtcNow,
            };

            _dbSetMock.Setup(m => m.Add(It.IsAny<Member>())).Callback<Member>(Members.Add);
            _contextMock.Setup(m => m.Members).Returns(_dbSetMock.Object);

            await _repository.Add(Member, cancellationToken);

            _dbSetMock.Verify(x => x.Add(It.Is<Member>(a => a.Id == "id1")), Times.Exactly(1));          
            _contextMock.Verify(context => context.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task GetAllMembers()
        {
            CancellationToken cancellationToken = new CancellationToken();
            var target = new MemberRepo(_contextMock.Object);
            var result = await target.GetAll(cancellationToken);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateMember()
        {           
         }

        [Test]
        public async Task GetMemberByIdWithExistingId()
        {         
        }

        [Test]
        public async Task GetMemberByIdWithNotExistingId()
        {           
        }

        [Test]
        public async Task DeleteMemberWithExistingId()
        {
        }
      
        private List<Member> CreateDataForTest()
        {
            var Member = new Member()
            {
                //Id = "id1",
                //Title = "t2",
                //Author = "a1",
                //PublicationYear = DateTime.UtcNow,
            };

            return new List<Member>() { Member };
        }      
    }

}