using System.Text.Json.Serialization;

namespace EP.DOMAIN
{
    public class Composer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime? Birthday { get; set; }

        [JsonIgnore]
        public ICollection<Piece> Pieces { get; set; }
    }
}