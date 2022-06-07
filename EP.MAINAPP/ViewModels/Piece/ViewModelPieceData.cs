using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.MAINAPP.ViewModels.Piece
{
    public class ViewModelPieceData : AbstractViewModelContainer
    {
        public ViewModelPieceData() : base()
        {
            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListPieces = await ac.GetAllPieces();
                OnPropertyChanged(nameof(ListPieces));
            }
        }
    }
}
