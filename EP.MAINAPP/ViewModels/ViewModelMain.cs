using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        #region DeledateCommand

        #region Composer
        public DelegateCommand DisplayComposerCommand { get; set; }
        public DelegateCommand DisplayCreateComposerCommand { get; set; }
        public DelegateCommand DisplayUpdateComposerCommand { get; set; }
        public DelegateCommand DeleteComposerCommand { get; set; }
        public DelegateCommand ConfirmComposerCommand { get; set; }
        #endregion


        #endregion
        public DelegateCommand DisplayArtistCommand { get; set; }
        
        public DelegateCommand DisplayLChartCommand { get; set; }
        public DelegateCommand DisplayPieceCommand { get; set; }
        public DelegateCommand DisplayTypeCommand { get; set; }

        


        //CRUD Composer

        public ViewModelMain()
        {
            DisplayArtistCommand = new DelegateCommand(DisplayArtistStartup);
            
            DisplayLChartCommand = new DelegateCommand(DisplayLChart);
            DisplayPieceCommand = new DelegateCommand(DisplayPieceStartup);
            DisplayTypeCommand = new DelegateCommand(DisplayTypeStartup);

            //Composer
            DisplayComposerCommand = new DelegateCommand(DisplayComposerStartup);
            DisplayCreateComposerCommand = new DelegateCommand(DisplayCreateComposer);
            DisplayUpdateComposerCommand = new DelegateCommand(DisplayUpdateComposer);
            DeleteComposerCommand = new DelegateCommand(DeleteComposer);


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

        //Methode Composer
        private void DisplayCreateComposer()
        {
            this.ViewModel = new ViewModelCreateComposer();
            ConfirmComposerCommand = new DelegateCommand(CreateComposer);
        }

        private void DisplayUpdateComposer()
        {
            this.ViewModel = new ViewModelUpdateComposer();
            ConfirmComposerCommand = new DelegateCommand(UpdateComposer);
        }

        private void CreateComposer()
        {
            var newComposer = ((ViewModelCreateComposer)this.ViewModel).Composer;
            var result = (this.ViewModel as ViewModelCreateComposer).AddComposerMethode(newComposer);

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{newComposer.FirstName }  {newComposer.LastName} is added to DB");
                DisplayComposerStartup();
            }

        }

        private void UpdateComposer()
        {
            throw new NotImplementedException();
        }

        private void DeleteComposer()
        {

        }
    }
}
