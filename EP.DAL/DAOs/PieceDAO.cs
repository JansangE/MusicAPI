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
    public class PieceDAO : IPiece
    {
        public async Task<Piece> GetByIdAsync(int id)
        {
            using MusicContext mc = new();
            var piece = await mc.Pieces
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.ID == id);

            return piece;
        }

        public async Task<IEnumerable<Piece>> GetAsync(int skip, int take)
        {
            using MusicContext sc = new();
            return await sc.Pieces
                .AsNoTracking()
                .OrderBy(x => x.ID)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            using MusicContext mc = new();
            return mc.Pieces.Count();
        }

        public async Task<Piece> CreateAsync(Piece entity)
        {
            using MusicContext mc = new();
            mc.Pieces.Add(entity);

            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Piece>> CreateRangeAsync(List<Piece> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }

            return entities;
        }

        public async Task<Piece> UpdateAsync(Piece entity)
        {
            using MusicContext mc = new();
            mc.Pieces.Attach(entity);
            mc.Entry<Piece>(entity).State = EntityState.Modified;

            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<Piece> DeleteAsync(Piece entity)
        {
            using MusicContext mc = new();

            mc.Pieces.Attach(entity);
            mc.Pieces.Remove(entity);

            await mc.SaveChangesAsync();

            return entity;
        }
    }
}
