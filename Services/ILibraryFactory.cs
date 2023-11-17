using LibraryApp.Repository;

namespace Services
{
    public interface ILibraryFactory
    {
        IBookRepo bookRepo { get; }
    }
}
