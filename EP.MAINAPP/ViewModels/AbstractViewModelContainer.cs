using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EP.MAINAPP.ViewModels
{
    public abstract class AbstractViewModelContainer : ViewModelBase
    {
        

        private DOMAIN.Composer _composer;
        protected DOMAIN.Composer _SelectedComposer;

        private DOMAIN.Artist _Artist;
        protected DOMAIN.Artist _selectedArtist;

        private DOMAIN.Type _Type;
        protected DOMAIN.Type _selectedType;

        private DOMAIN.Piece _Piece;
        protected DOMAIN.Piece _selectedPiece;

        public DOMAIN.Composer Composer
        {
            get => _composer;
            set
            {
                _composer = value;
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

        public DOMAIN.Artist Artist
        {
            get => _Artist;
            set
            {
                _Artist = value;
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

        public DOMAIN.Type Type
        {
            get => _Type;
            set
            {
                _Type = value;
                OnPropertyChanged();
            }
        }
        public DOMAIN.Type SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }

        public DOMAIN.Piece Piece
        {
            get => _Piece;
            set
            {
                _Piece = value;
                OnPropertyChanged();
            }
        }
        public DOMAIN.Piece SelectedPiece
        {
            get => _selectedPiece;
            set
            {
                _selectedPiece = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<DOMAIN.Composer> ListComposers { get; set; }
        public ObservableCollection<DOMAIN.Artist> ListArtists { get;  set; }
        public ObservableCollection<DOMAIN.Type> ListTypes { get; set; }
        public ObservableCollection<DOMAIN.Piece> ListPieces { get; set; }


        public Visibility IsReady { get; set; }

        public AbstractViewModelContainer() : base()
        {
            //ListComposers = new ObservableCollection<DOMAIN.Composer>();
        }
    }
}
