using LibraryApp.Repository.DataModel;

namespace LibraryApp.Repository
{
    public interface IMemberRepo
    {
        Task<List<Member>> GetAll(CancellationToken cancellationToken);
        Task<Member> GetById(string id, CancellationToken cancellationToken);
        Task Add(Member member, CancellationToken cancellationToken);
        Task Update(string id, Member member, CancellationToken cancellationToken);
        Task Delete(Member member, CancellationToken cancellationToken);
    }
}
