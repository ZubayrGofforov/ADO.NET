using adonet_db.Models;

namespace adonet_db.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<int> CreateAsync(User user);

        public Task<int> UpdateAsync(long id, User user);

        public Task<int> DeleteAsync(long id);

        public Task<User?> GetAsync(long id);

        public Task<IEnumerable<User>> GetAllAsync(int skip, int take);
    }
}
