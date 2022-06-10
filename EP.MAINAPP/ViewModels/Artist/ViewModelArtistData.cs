using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EP.MAINAPP.ViewModels.Artist
{
    public class ViewModelArtistData : AbstractViewModelContainer
    {
        public ViewModelArtistData() : base()
        {
            IsReady = Visibility.Hidden;
            OnPropertyChanged(nameof(IsReady));
            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListArtists = await ac.GetAllArtists();
                OnPropertyChanged(nameof(ListArtists));
            }

            IsReady = Visibility.Visible;
            OnPropertyChanged(nameof(IsReady));
        }
    }
}
