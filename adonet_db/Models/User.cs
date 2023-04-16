namespace adonet_db.Models
{
    public class User
    {
        public long Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
