using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.DOMAIN
{
    public class PieceArtist
    {
        public int PieceID { get; set; }
        public int ArtistID { get; set; }

        public Piece Piece { get; set; }
        public Artist Artist { get; set; }
    }
}
