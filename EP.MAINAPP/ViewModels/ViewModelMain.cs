using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;
using EP.MAINAPP.ViewModels.Artist;
using EP.MAINAPP.ViewModels.Composer;
using EP.MAINAPP.ViewModels.LChart;
using EP.MAINAPP.ViewModels.Piece;
using EP.MAINAPP.ViewModels.Type;
using Prism.Commands;

namespace EP.MAINAPP.ViewModels
{
    public class ViewModelMain : ViewModelBase
    {
        private ViewModelBase _viewModel;

        public DelegateCommand DisplayArtistCommand { get; set; }
        public DelegateCommand DisplayComposerCommand { get; set; }
        public DelegateCommand DisplayLChartCommand { get; set; }
        public DelegateCommand DisplayPieceCommand { get; set; }
        public DelegateCommand DisplayTypeCommand { get; set; }

        public DelegateCommand DisplayCreateUpdateComposerCommand { get; set; }


        //CRUD Composer

        public ViewModelMain()
        {
            DisplayArtistCommand = new DelegateCommand(DisplayArtistStartup);
            DisplayComposerCommand = new DelegateCommand(DisplayComposerStartup);
            DisplayLChartCommand = new DelegateCommand(DisplayLChart);
            DisplayPieceCommand = new DelegateCommand(DisplayPieceStartup);
            DisplayTypeCommand = new DelegateCommand(DisplayTypeStartup);

            DisplayCreateUpdateComposerCommand = new DelegateCommand(DisplayCreateUpdateComposer);

            //CRUD Composer
            
        }


        public ViewModelBase ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged();
            }
        }
        

        private void DisplayArtistStartup()
        {
            this.ViewModel = new ViewModelArtistData();
        }

        private void DisplayComposerStartup()
        {
            this.ViewModel = new ViewModelComposerData();
        }

        private void DisplayLChart()
        {
            this.ViewModel = new ViewModelLChart();
        }

        private void DisplayPieceStartup()
        {
            this.ViewModel = new ViewModelPieceData();
        }

        private void DisplayTypeStartup()
        {
            this.ViewModel = new ViewModelTypeData();
        }

        private void DisplayCreateUpdateComposer()
        {

            this.ViewModel = new ViewModelCreateUpdateComposer();
        }

    }
}
