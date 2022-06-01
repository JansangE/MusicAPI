using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;
using EP.DOMAIN.Contracts;
using Microsoft.EntityFrameworkCore;
using Type = EP.DOMAIN.Type;

namespace EP.DAL
{
    public class TypeDAO : IType
    {
        public async Task<Type> GetByIdAsync(int id)
        {
            using MusicContext mc = new();
            var type = await mc.Types
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.ID == id);

            return type;
        }

        public async Task<IEnumerable<Type>> GetAsync(int skip, int take)
        {
            using MusicContext sc = new();
            return await sc.Types
                .AsNoTracking()
                .OrderBy(x => x.ID)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            using MusicContext mc = new();
            return mc.Types.Count();
        }

        public async Task<Type> CreateAsync(Type entity)
        {
            using MusicContext mc = new();
            mc.Types.Add(entity);

            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Type>> CreateRangeAsync(List<Type> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i] = await CreateAsync(entities[i]);
            }

            return entities;
        }

        public async Task<Type> UpdateAsync(Type entity)
        {
            using MusicContext mc = new();
            mc.Types.Attach(entity);
            mc.Entry<Type>(entity).State = EntityState.Modified;

            await mc.SaveChangesAsync();

            return entity;
        }

        public async Task<Type> DeleteAsync(Type entity)
        {
            using MusicContext mc = new();

            mc.Types.Attach(entity);
            mc.Types.Remove(entity);

            await mc.SaveChangesAsync();

            return entity;
        }
    }
}
