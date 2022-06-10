using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EP.MAINAPP.ViewModels.Piece
{
    public class ViewModelPieceData : AbstractViewModelContainer
    {
        public ViewModelPieceData() : base()
        {
            IsReady = Visibility.Hidden;
            OnPropertyChanged(nameof(IsReady));

            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListPieces = await ac.GetAllPieces();
                OnPropertyChanged(nameof(ListPieces));

                ListComposers = await ac.GetAllComposer();
                OnPropertyChanged(nameof(ListComposers));

                ListTypes = await ac.GetAllTypes();
                OnPropertyChanged(nameof(ListTypes));
            }

            IsReady = Visibility.Visible;
            OnPropertyChanged(nameof(IsReady));
        }
    }
}
