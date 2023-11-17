using LibraryApp.Repository;

namespace Services
{
    public class LibraryFactory : ILibraryFactory
    {
        private IBookRepo? _bookRepo;
        private readonly LibraryContext _libraryContext;

        public LibraryFactory(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public IBookRepo bookRepo => _bookRepo ?? new BookRepo(_libraryContext);
    }
}
