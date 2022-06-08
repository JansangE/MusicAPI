using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DAL;
using EP.DOMAIN;

namespace EP.BLL
{
    public class ArtistManager
    {
        private readonly ArtistDAO _artistDao = new ArtistDAO();

        public async Task<int> GetTotalCountAsync()
        {
            return await _artistDao.GetTotalCountAsync();
        }
        public async Task<Artist> CreateAsync(Artist entity)
        {
            return await _artistDao.CreateAsync(entity);
        }

        public async Task<IEnumerable<Artist>> GetAsync(int skip, int take)
        {
            return await _artistDao.GetAsync(skip, take);
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            return await _artistDao.GetByIdAsync(id);
        }

        public async Task<Artist> UpdateAsync(int id, Artist entity)
        {
            entity.ID = id;
            return await _artistDao.UpdateAsync(entity);
        }

        public async Task<Artist> DeleteAsync(int id)
        {
            var entity = await _artistDao.GetByIdAsync(id);

            return await _artistDao.DeleteAsync(entity);
        }
    }
}
