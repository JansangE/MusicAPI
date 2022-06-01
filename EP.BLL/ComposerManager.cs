using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DAL;
using EP.DOMAIN;

namespace EP.BLL
{
    public class ComposerManager
    {
        private readonly ComposerDAO _composerDAO = new ComposerDAO();

        public async Task<Composer> CreateAsync(Composer entity)
        {
            //if (!IsValidEmail(entity.Email))
            //{
            //    Task<Student> exTask = new(() =>
            //    {
            //        throw new Exception("invalid email");
            //    });
            //    exTask.RunSynchronously();
            //    return await exTask;
            //}

            return await _composerDAO.CreateAsync(entity);
        }

        public async Task<IEnumerable<Composer>> GetAsync(int skip, int take)
        {
            return await _composerDAO.GetAsync(skip, take);
        }

        //async Task<Composer> IComposer.GetByIdAsync(int id)
        //{
        //    using MusicContext mc = new();
        //    var composer = await mc.Composers
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(c => c.ID == id);
        //    return composer;
        //}

        public async Task<Composer> GetByIdAsync(int id)
        {
            return await _composerDAO.GetByIdAsync(id);
        }

        public async Task<Composer> UpdateAsync(int id, Composer entity)
        {
            entity.ID = id;
            return await _composerDAO.UpdateAsync(entity);
        }

        public async Task<Composer> DeleteAsync(int id)
        {
            var entity = await _composerDAO.GetByIdAsync(id);
            return await _composerDAO.DeleteAsync(entity);
        }
    }
}
