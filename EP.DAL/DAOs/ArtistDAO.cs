using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;
using EP.DOMAIN.Contracts;
using Microsoft.EntityFrameworkCore;
using NotImplementedException = System.NotImplementedException;

namespace EP.DAL
{
    public class ArtistDAO : IArtist
    {

        public async Task<Artist> GetByIdAsync(int id)
        {
            using MusicContext mc = new();
            var artist = await mc.Artists
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.ID == id);

            return artist;
        }

        public async Task<IEnumerable<Artist>> GetAsync(int skip, int take)
        {
            using MusicContext sc = new();
            return await sc.Artists
                .AsNoTracking()
                .OrderBy(x => x.ID)
                //.Skip(skip)
                //.Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            using MusicContext mc = new();
            return mc.Artists.Count();
        }

        public async Task<Artist> CreateAsync(Artist entity)
        {
            using MusicContext mc = new();
            mc.Artists.Add(entity);

            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Artist>> CreateRangeAsync(List<Artist> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }

            return entities;
        }

        public async Task<Artist> UpdateAsync(Artist entity)
        {
            using MusicContext mc = new();
            mc.Artists.Attach(entity);
            mc.Entry<Artist>(entity).State = EntityState.Modified;

            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<Artist> DeleteAsync(Artist entity)
        {
            using MusicContext mc = new();

            mc.Artists.Attach(entity);
            mc.Artists.Remove(entity);

            await mc.SaveChangesAsync();

            return entity;
        }
    }
}
