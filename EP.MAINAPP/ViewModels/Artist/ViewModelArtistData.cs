using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.MAINAPP.ViewModels.Artist
{
    public class ViewModelArtistData : AbstractViewModelContainer
    {
        public ViewModelArtistData() : base()
        {
            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListArtists = await ac.GetAllArtists();
                OnPropertyChanged(nameof(ListArtists));
            }
        }
    }
}
