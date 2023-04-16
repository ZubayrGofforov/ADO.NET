using adonet_db.Constans;
using adonet_db.Enums;
using adonet_db.Interfaces.Repositories;
using adonet_db.Models;
using Npgsql;

namespace adonet_db.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly NpgsqlConnection _npgsqlConnection = new NpgsqlConnection(DbConstans.CONNECTION_STRING);
        public async Task<int> CreateAsync(Word word)
        {
            try
            {
                await _npgsqlConnection.OpenAsync();
                string query = "INSERT INTO words(word_, translate_, level_, count_)" +
                    $"VALUES ('{word.Word_}', '{word.Translate}, '{word.Level}, {word.Count}')";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _npgsqlConnection);
                int result = await npgsqlCommand.ExecuteNonQueryAsync();
                return result;
            }
            catch { return 0; }
            finally { await _npgsqlConnection.CloseAsync(); }
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _npgsqlConnection.OpenAsync();
                string query = $"DELETE FROM words WHERE id = {id}";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _npgsqlConnection);
                int result = await npgsqlCommand.ExecuteNonQueryAsync();
                return result;
            }
            catch { return 0; }
            finally { await _npgsqlConnection.CloseAsync(); }
        }

        public async Task<IEnumerable<Word>> GetAllAsync(int skip, int take)
        {
            _npgsqlConnection.OpenAsync();
            string query = $"SELECT * FROM words OFFSET {skip} LIMIT {take}; ";
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _npgsqlConnection);
            var reader = await npgsqlCommand.ExecuteReaderAsync();
            IList<Word> words = new List<Word>();
            while (await reader.ReadAsync())
            {
                Word word = new Word()
                {
                    Id = reader.GetInt32(0),
                    Word_ = reader.GetString(1),
                    Translate = reader.GetString(2),
                    Level = (Level)Enum.Parse(typeof(Level), reader.GetString(3)),
                    Count = reader.GetInt32(4)
                };

                words.Add(word);
            }

            return words;
        }

        public async Task<int> UpdateAsync(long id, Word word)
        {
            try
            {
                await _npgsqlConnection.OpenAsync();
                string query = $"UPDATE words" +
                    $"SET word_ = '{word.Word_}', translate_ = '{word.Translate}'," +
                    $"SET level_ = {word.Level}, count_ = {word.Count}";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _npgsqlConnection);
                int result = await npgsqlCommand.ExecuteNonQueryAsync();
                return result;
            }
            catch { return 0; }
            finally { await _npgsqlConnection.CloseAsync(); }
        }
    }
}
