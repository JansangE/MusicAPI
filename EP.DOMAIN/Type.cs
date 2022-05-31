namespace EP.DOMAIN
{
    public class Type
    {
        public int ID { get; set; }
        public string NameType { get; set; }


        public ICollection<Piece> Pieces { get; set; }
    }
}