using Repository;

namespace Services
{
    public class LibraryFactory : ILibraryFactory
    {
        private IBookRepo? _bookRepo;
        private IMemberRepo? _memberRepo;
        private IBorrowTransactionRepo? _borrowTransactionRepo;
        private readonly LibraryContext _libraryContext;

        public LibraryFactory(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public IBookRepo bookRepo => _bookRepo ?? new BookRepo(_libraryContext);
        public IMemberRepo memberRepo => _memberRepo ?? new MemberRepo(_libraryContext);
        public IBorrowTransactionRepo borrowTransactionRepo => _borrowTransactionRepo ?? new BorrowTransactionRepo(_libraryContext);
    }
}
