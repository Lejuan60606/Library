using Repository;

namespace Services
{
    public interface ILibraryFactory
    {
        IBookRepo bookRepo { get; }
        IMemberRepo memberRepo { get; }
        IBorrowTransactionRepo borrowTransactionRepo { get; }
    }
}
