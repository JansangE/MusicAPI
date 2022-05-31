using EP.DAL;
using EP.DOMAIN;

namespace EP.BLL
{
    public class MusicManager
    {
        private readonly MusicDAO mDAO = new MusicDAO();

        public async Task<Piece> CreateAsync(Piece entity)
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

            return await mDAO.CreateAsync(entity);
        }

        public async Task<IEnumerable<Piece>> GetAsync(int skip, int take)
        {
            return await mDAO.GetAsync(skip, take);
        }


    }
}