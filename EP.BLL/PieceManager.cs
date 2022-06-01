using EP.DAL;
using EP.DOMAIN;

namespace EP.BLL
{
    public class PieceManager
    {
        private readonly PieceDAO _pieceDao = new PieceDAO();

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

            return await _pieceDao.CreateAsync(entity);
        }

        public async Task<IEnumerable<Piece>> GetAsync(int skip, int take)
        {
            return await _pieceDao.GetAsync(skip, take);
        }

        public async Task<Piece> GetByIdAsync(int id)
        {
            return await _pieceDao.GetByIdAsync(id);
        }

        public async Task<Piece> UpdateAsync(int id, Piece entity)
        {
            entity.ID = id;
            return await _pieceDao.UpdateAsync(entity);
        }

        public async Task<Piece> DeleteAsync(int id)
        {
            var entity = await _pieceDao.GetByIdAsync(id);

            return await _pieceDao.DeleteAsync(entity);
        }

    }
}