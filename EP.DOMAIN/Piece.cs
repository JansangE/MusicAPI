namespace EP.DOMAIN
{
    public class Piece
    {
        public int ID { get; set; }
        public string NamePiece { get; set; }
        public int ComposerID { get; set; }
        public int TypeID { get; set; }

        public Composer Composer { get; set; }
        public Type Type { get; set; }
        public ICollection<PieceArtist> Artists { get; set; }
    }
}