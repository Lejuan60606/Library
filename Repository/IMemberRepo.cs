using LibraryApp.Repository.DataModel;

namespace LibraryApp.Repository
{
    public interface IMemberRepo
    {
        Task<IList<Member>> GetAll();
        Task<Member> GetById(string id);
        Task<bool> Add(Member member);
        Task<bool> Update(Member member);
        Task<bool> Delete(string id);
    }
}
