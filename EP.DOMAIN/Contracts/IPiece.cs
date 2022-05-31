using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.DOMAIN.Contracts
{
    public interface IPiece : IGeneric<Piece>
    {
        Task<Piece> GetByIdAsync(int id);
    }
}
