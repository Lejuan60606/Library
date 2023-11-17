using LibraryApp.Repository.DataModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repository
{
    public class MemberRepo : IMemberRepo
    {
        private readonly LibraryContext _dbContext;

        public MemberRepo(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Member Member, CancellationToken cancellationToken)
        {
            _dbContext.Members.Add(Member);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Member Member, CancellationToken cancellationToken)
        {
            _dbContext.Members.Remove(Member);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Member>> GetAll(CancellationToken cancellationToken)
        {
            return await _dbContext.Members.ToListAsync(cancellationToken);
        }

        public async Task<Member> GetById(string id, CancellationToken cancellationToken)
        {
            return await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == id, cancellationToken: cancellationToken);
        }

        public async Task Update(string id, Member member, CancellationToken cancellationToken)
        {
            var existingMember = await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == member.Id, cancellationToken: cancellationToken);
            if (existingMember != null)
            {
                existingMember.Name = member.Name;               
                existingMember.JoinedDate = member.JoinedDate;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
