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
            this.ViewModel = new ViewModelCRUDComposer();
            ConfirmComposerCommand = new DelegateCommand(CreateComposer);
        }

        private void DisplayUpdateComposer()
        {
            var selectedComposer = ((AbstractViewModelContainer)this.ViewModel).SelectedComposer;
            if (selectedComposer is not null)
            {
                this.ViewModel = new ViewModelCRUDComposer(selectedComposer);
                ConfirmComposerCommand = new DelegateCommand(UpdateComposer);
            }
            else
            {
                MessageBox.Show("Please select a composer that you want to update");
            }
            //this.ViewModel = new ViewModelUpdateComposer();
           
        }

        private void CreateComposer()
        {
            var newComposer = ((ViewModelCRUDComposer)this.ViewModel).Composer;
            var result = (this.ViewModel as ViewModelCRUDComposer).AddComposerMethode(newComposer);

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

        private async void UpdateComposer()
        {
            var selectedComposer = ((ViewModelCRUDComposer)this.ViewModel).Composer;

            var result = ((ViewModelCRUDComposer)this.ViewModel).UpdateComposerMethode(selectedComposer);

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {

                MessageBox.Show($"{selectedComposer.FirstName }  {selectedComposer.LastName} is updated to DB");
                DisplayComposerStartup();
            }
        }

        private async void DeleteComposer()
        {
            var selectedComposer = ((AbstractViewModelContainer)this.ViewModel).SelectedComposer;
            

            if (selectedComposer is null)
            {
                MessageBox.Show("PLease select a composer that you want to delete");
            }
            else
            {
                this.ViewModel = new ViewModelCRUDComposer(selectedComposer);

                var result = ((ViewModelCRUDComposer)this.ViewModel).DeleteComposerMethode(selectedComposer);

                MessageBox.Show($"{selectedComposer.FirstName}  {selectedComposer.LastName} is deleted from DB");
                DisplayComposerStartup();

            }


        }
    }
}
