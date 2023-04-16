using adonet_db.Enums;

namespace adonet_db.Models
{
    public class Word
    {
        public long Id { get; set; }

        public string Word_ { get; set; } = string.Empty;

        public string Translate { get; set; } = string.Empty;

        public int Count { get; set; }

        public Level Level { get; set; }
    }
}
