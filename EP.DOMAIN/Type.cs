using System.Text.Json.Serialization;

namespace EP.DOMAIN
{
    public class Type
    {
        public int ID { get; set; }
        public string NameType { get; set; }

        [JsonIgnore]
        public ICollection<Piece> Pieces { get; set; }
    }
}