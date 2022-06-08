using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DAL;
using EP.DOMAIN;
using Type = EP.DOMAIN.Type;

namespace EP.BLL
{
    public class TypeManager
    {
        private readonly TypeDAO _typeDao = new TypeDAO();

        public async Task<int> GetTotalCountAsync()
        {
            return await _typeDao.GetTotalCountAsync();
        }
        public async Task<Type> CreateAsync(Type entity)
        {
            return await _typeDao.CreateAsync(entity);
        }

        public async Task<IEnumerable<Type>> GetAsync(int skip, int take)
        {
            return await _typeDao.GetAsync(skip, take);
        }

        public async Task<Type> GetByIdAsync(int id)
        {
            return await _typeDao.GetByIdAsync(id);
        }

        public async Task<Type> UpdateAsync(int id, Type entity)
        {
            entity.ID = id;
            return await _typeDao.UpdateAsync(entity);
        }

        public async Task<Type> DeleteAsync(int id)
        {
            var entity = await _typeDao.GetByIdAsync(id);

            return await _typeDao.DeleteAsync(entity);
        }
    }
}
