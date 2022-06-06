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

        #region Artist
        public DelegateCommand DisplayArtistCommand { get; set; }
        public DelegateCommand DisplayCreateArtistCommand { get; set; }
        public DelegateCommand DisplayUpdateArtistCommand { get; set; }
        public DelegateCommand DeleteArtistCommand { get; set; }
        public DelegateCommand ConfirmArtistCommand { get; set; }
        #endregion


        #endregion

        public DelegateCommand DisplayLChartCommand { get; set; }
        public DelegateCommand DisplayPieceCommand { get; set; }
        public DelegateCommand DisplayTypeCommand { get; set; }

        


        //CRUD Composer

        public ViewModelMain()
        {

            DisplayLChartCommand = new DelegateCommand(DisplayLChart);
            DisplayPieceCommand = new DelegateCommand(DisplayPieceStartup);
            DisplayTypeCommand = new DelegateCommand(DisplayTypeStartup);

            //Composer
            DisplayComposerCommand = new DelegateCommand(DisplayComposerStartup);
            DisplayCreateComposerCommand = new DelegateCommand(DisplayCreateComposer);
            DisplayUpdateComposerCommand = new DelegateCommand(DisplayUpdateComposer);
            DeleteComposerCommand = new DelegateCommand(DeleteComposer);

            //Artist
            DisplayArtistCommand = new DelegateCommand(DisplayArtistStartup);
            DisplayCreateArtistCommand = new DelegateCommand(DisplayCreateArtist);
            DisplayUpdateArtistCommand = new DelegateCommand(DisplayUpdateArtist);
            DeleteArtistCommand = new DelegateCommand(DeleteArtist);


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

        #region CRUD Composer

        

        
        //CRUD Composer
        private void DisplayCreateComposer()
        {
            this.ViewModel = new ViewModelCreateUpdateComposer();
            ConfirmComposerCommand = new DelegateCommand(CreateComposer);
        }

        private void DisplayUpdateComposer()
        {
            var selectedComposer = ((AbstractViewModelContainer)this.ViewModel).SelectedComposer;
            if (selectedComposer is not null)
            {
                this.ViewModel = new ViewModelCreateUpdateComposer(selectedComposer);
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
            var newComposer = ((ViewModelCreateUpdateComposer)this.ViewModel).Composer;
            var result = (this.ViewModel as ViewModelCreateUpdateComposer).AddComposerMethode(newComposer);

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
            var selectedComposer = ((ViewModelCreateUpdateComposer)this.ViewModel).Composer;

            var result = ((ViewModelCreateUpdateComposer)this.ViewModel).UpdateComposerMethode(selectedComposer);

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

            if (selectedComposer is not null)
            {
                if (MessageBox.Show($"Are you sure you want to delete {selectedComposer.NickName}?",
                        "Delete composer",
                        MessageBoxButton.YesNo, 
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string result = null;

                    using (ApiCall ac = new ApiCall())
                    {
                        result = await ac.DeleteComposer(selectedComposer);
                    }

                    MessageBox.Show($"{selectedComposer.NickName} is deleted from DB");
                    DisplayComposerStartup();
                }
                
            }
            else
            {
                MessageBox.Show("Please select a composer that you want to remove");
            }
            
        }
        #endregion

        #region CRUD Artist
        //CRUD Artist
        private void DisplayCreateArtist()
        {
            this.ViewModel = new ViewModelCreateUpdateArtist();
            ConfirmArtistCommand = new DelegateCommand(CreateArtist);
        }

        private void DisplayUpdateArtist()
        {
            var selectedArtist = ((AbstractViewModelContainer)this.ViewModel).SelectedArtist;
            if (selectedArtist is not null)
            {
                this.ViewModel = new ViewModelCreateUpdateArtist(selectedArtist);
                ConfirmArtistCommand = new DelegateCommand(UpdateArtist);
            }
            else
            {
                MessageBox.Show("Please select an artist that you want to update");
            }
            

        }

        private void CreateArtist()
        {
            var newArtist = ((ViewModelCreateUpdateArtist)this.ViewModel).Artist;
            var result = (this.ViewModel as ViewModelCreateUpdateArtist).AddArtistMethode(newArtist);

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{newArtist.NameArtist } is added to DB");
                DisplayArtistStartup();
            }

        }

        private async void UpdateArtist()
        {
            var selectedArtist = ((ViewModelCreateUpdateArtist)this.ViewModel).Artist;

            var result = ((ViewModelCreateUpdateArtist)this.ViewModel).UpdateArtistMethode(selectedArtist);

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{selectedArtist.NameArtist } is updated to DB");
                DisplayArtistStartup();
            }

        }

        private async void DeleteArtist()
        {
            var selectedArtist = ((AbstractViewModelContainer)this.ViewModel).SelectedArtist;

            if (selectedArtist is not null)
            {
                if (MessageBox.Show($"Are you sure you want to delete {selectedArtist.NameArtist}?",
                        "Delete artist",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string result = null;

                    using (ApiCall ac = new ApiCall())
                    {
                        result = await ac.DeleteArtist(selectedArtist);
                    }

                    MessageBox.Show($"{selectedArtist.NameArtist} is deleted from DB");
                    DisplayArtistStartup();
                }

            }
            else
            {
                MessageBox.Show("Please select a artist that you want to remove");
            }

        }
        #endregion
    }
}
