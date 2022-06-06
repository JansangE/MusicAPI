using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.MAINAPP.ViewModels
{
    public abstract class AbstractViewModelContainer : ViewModelBase
    {
        private DOMAIN.Composer _composer;
        protected DOMAIN.Composer _SelectedComposer;

        private DOMAIN.Artist _Artist;
        protected DOMAIN.Artist _selectedArtist;

        public DOMAIN.Composer Composer
        {
            get => _composer;
            set
            {
                _composer = value;
                OnPropertyChanged();
            }
        }

        public DOMAIN.Artist SelectedArtist
        {
            get => _selectedArtist;
            set
            {
                _selectedArtist = value;
                OnPropertyChanged();
            }
        }

        public DOMAIN.Artist Artist
        {
            get => _Artist;
            set
            {
                _Artist = value;
                OnPropertyChanged();
            }
        }

        public DOMAIN.Composer SelectedComposer
        {
            get => _SelectedComposer;
            set
            {
                _SelectedComposer = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DOMAIN.Composer> ListComposers
        {
            get;
            set;
        }

        public ObservableCollection<DOMAIN.Artist> ListArtists
        {
            get;
            set;
        }



        public AbstractViewModelContainer() : base()
        {
            //ListComposers = new ObservableCollection<DOMAIN.Composer>();
        }
    }
}
