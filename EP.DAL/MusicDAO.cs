using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;
using EP.DOMAIN.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EP.DAL
{
    public class MusicDAO : IPiece
    {
        Task<Piece> IGeneric<Piece>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<Piece> IPiece.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Piece>> GetAsync(int skip, int take)
        {
            using MusicContext sc = new();
            return await sc.Pieces.AsNoTracking().OrderBy(x => x.ID).Skip(skip).Take(take).ToListAsync();
        }

        public Task<int> GetTotalCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Piece> CreateAsync(Piece entity)
        {
            using MusicContext mc = new();
            mc.Pieces.Add(entity);

            await mc.SaveChangesAsync();

            return entity;
        }

        public Task<IEnumerable<Piece>> CreateRangeAsync(List<Piece> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Piece> UpdateAsync(Piece entity)
        {
            throw new NotImplementedException();
        }

        public Task<Piece> DeleteAsync(Piece entity)
        {
            throw new NotImplementedException();
        }
    }
}
