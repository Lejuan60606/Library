using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesForTestEFCore
{
    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>
    {
        public TestAsyncEnumerable(Expression expression)
       : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) =>
            new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}
