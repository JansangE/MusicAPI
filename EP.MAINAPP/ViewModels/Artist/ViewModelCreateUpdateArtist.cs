using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;

namespace EP.MAINAPP.ViewModels.Artist
{
    public class ViewModelCreateUpdateArtist : AbstractViewModelContainer
    {
        public ViewModelCreateUpdateArtist()
        {
            Artist = new DOMAIN.Artist();
        }

        public ViewModelCreateUpdateArtist(DOMAIN.Artist artist)
        {
            GetArtist(artist);
        }

        private async void GetArtist(DOMAIN.Artist artist)
        {
            using (ApiCall ac = new ApiCall())
            {
                Artist = await ac.GetSelectedArtist(artist);
            }

        }

        public async Task<string> AddArtistMethode(DOMAIN.Artist artist)
        {
            string result = null;

            if (AllRequired(artist))
            {
                using (ApiCall ac = new ApiCall())
                {
                    artist = await ac.AddArtist(artist);
                    OnPropertyChanged(nameof(ListArtists));
                }

                if (artist is not null)
                {
                    result = $"{artist.NameArtist} is added in da DB";
                }
                else
                {
                    result = $"Something went wrong with API";
                }
            }
            else
            {
                result = "Some field is not correct";
            }

            return result;
        }

        public async Task<string> UpdateArtistMethode(DOMAIN.Artist artist)
        {
            string result = null;

            if (AllRequired(artist))
            {
                using (ApiCall ac = new ApiCall())
                {
                    artist = await ac.UpdateArtist(artist);
                    OnPropertyChanged(nameof(ListArtists));
                }

                if (artist is not null)
                {
                    result = $"{artist.NameArtist} is updated in da DB";
                }
                else
                {
                    result = $"Something went wrong with API";
                }
            }
            else
            {
                result = "Some field is not correct";
            }

            return result;
        }

        private bool AllRequired(DOMAIN.Artist artist)
        {
            return true;
        }
    }
}
