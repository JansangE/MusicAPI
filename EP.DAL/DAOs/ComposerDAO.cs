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
    public class ComposerDAO : IComposer
    {

        public async Task<Composer> GetByIdAsync(int id)
        {
            using MusicContext mc = new();
            var composer = await mc.Composers
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ID == id);

            return composer;
        }

        public async Task<IEnumerable<Composer>> GetAsync(int skip, int take)
        {
            using MusicContext sc = new();
            return await sc.Composers
                .AsNoTracking()
                .OrderBy(x => x.ID)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            using MusicContext mc = new();
            return mc.Composers.Count();
        }

        public async Task<Composer> CreateAsync(Composer entity)
        {
            using MusicContext mc = new();
            mc.Composers.Add(entity);
            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Composer>> CreateRangeAsync(List<Composer> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }

            return entities;
        }

        public async Task<Composer> UpdateAsync(Composer entity)
        {
            using MusicContext mc = new();

            mc.Composers.Attach(entity);
            mc.Entry<Composer>(entity).State = EntityState.Modified;
            await mc.SaveChangesAsync();

            return entity;
        }

        //public async Task<Composer> UpdateAsync(int id,Composer entity)
        //{
        //    using MusicContext mc = new();

        //    var dbComposer = new Composer() { ID = id };
        //    mc.Composers.Attach(dbComposer);

        //    dbComposer.FirstName = entity.FirstName;
        //    dbComposer.LastName = entity.LastName;
        //    dbComposer.NickName = entity.NickName;
        //    dbComposer.Birtday = entity.Birtday;

        //    mc.SaveChangesAsync();

        //    return entity;

        //}

        public async Task<Composer> DeleteAsync(Composer entity)
        {
            using MusicContext mc = new();
            mc.Composers.Attach(entity);
            mc.Composers.Remove(entity);
            await mc.SaveChangesAsync();

            return entity;

        }
    }
}
