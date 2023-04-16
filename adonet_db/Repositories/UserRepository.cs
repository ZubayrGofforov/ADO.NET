using adonet_db.Constans;
using adonet_db.Interfaces.Repositories;
using adonet_db.Models;
using Npgsql;

namespace adonet_db.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection = new NpgsqlConnection(DbConstans.CONNECTION_STRING);

        public async Task<int> CreateAsync(User user)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $@"INSERT INTO public.users(full_name, username, password_, address) 
                            VALUES(@full_name, @username, @password_, @address); ";
                NpgsqlCommand command = new NpgsqlCommand(query, _connection)
                {
                    Parameters = {
                        new("full_name", user.FullName),
                        new("username", user.Username),
                        new("password", user.Password),
                        new("address", user.Address)
                    }
                };

                int result = await command.ExecuteNonQueryAsync();
                return result;
            }
            catch { return 0; }
            finally { await _connection.CloseAsync(); }
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"DELETE FROM USERS WHERE ID = {id};";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _connection);
                int result = await npgsqlCommand.ExecuteNonQueryAsync();
                return result;
            }
            catch { return 0; }
            finally { await _connection.CloseAsync(); }
        }

        public async Task<IEnumerable<User>> GetAllAsync(int skip, int take)
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users OFFSET {skip} LIMIT {take};";
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _connection);
            var reader = await npgsqlCommand.ExecuteReaderAsync();
            IList<User> users = new List<User>();
            while (reader.Read())
            {
                User user = new User()
                {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Username = reader.GetString(2),
                    Password = reader.GetString(3),
                    Address = reader.GetString(4)
                };
                users.Add(user);
            }

            return users;
        }

        public async Task<User?> GetAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM users WHERE id = {id}; ";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _connection);
                var reader = await npgsqlCommand.ExecuteReaderAsync();
                var user = await reader.ReadAsync();
                if (user)
                {
                    return new User()
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Username = reader.GetString(2),
                        Password = reader.GetString(3),
                        Address = reader.GetString(4)
                    };
                }
                else return null;
            }
            catch { return null; }
            finally { await _connection.CloseAsync(); }
        }

        public async Task<int> UpdateAsync(long id, User user)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE users" +
                    $"SET full_name = '{user.FullName}', username = '{user.Username}'," +
                    $"password_ = '{user.Password}', address = '{user.Address}'";
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, _connection);
                int result = await npgsqlCommand.ExecuteNonQueryAsync();
                return result;
            }
            catch { return 0; }
            finally { await _connection.CloseAsync(); }
        }
    }
}