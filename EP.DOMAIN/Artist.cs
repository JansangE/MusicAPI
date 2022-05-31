using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.DOMAIN
{
    public class Artist
    {
        public int ID { get; set; }
        public string NameArtist { get; set; }

        public ICollection<PieceArtist> Pieces { get; set; }
    }
}
