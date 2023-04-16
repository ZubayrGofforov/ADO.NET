using adonet_db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db.Interfaces.Repositories
{
    public interface IWordRepository
    {
        public Task<IEnumerable<Word>> GetAllAsync(int skip, int take);

        public Task<int> CreateAsync(Word word);

        public Task<int> UpdateAsync(long id, Word word);

        public Task<int> DeleteAsync(long id);
    }
}
