using Microsoft.EntityFrameworkCore;
using Moq;

namespace UtilitiesForTestEFCore
{
    public static class MoqExtensions
    {
        public static Mock<DbSet<TEntity>> SetData<TEntity>(this Mock<DbSet<TEntity>> mock, IEnumerable<TEntity> data)
            where TEntity : class
        {
            var queryableData = data.AsQueryable();

            mock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<TEntity>(queryableData.Provider));
            mock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            mock.As<IAsyncEnumerable<TEntity>>().Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<TEntity>(queryableData.GetEnumerator()));

            return mock;
        }
    }
}