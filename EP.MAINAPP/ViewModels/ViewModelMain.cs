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
        private ViewModelBase _viewModel2;

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

        #region Type
        public DelegateCommand DisplayTypeCommand { get; set; }
        public DelegateCommand DisplayCreateTypeCommand { get; set; }
        public DelegateCommand DisplayUpdateTypeCommand { get; set; }
        public DelegateCommand DeleteTypeCommand { get; set; }
        public DelegateCommand ConfirmTypeCommand { get; set; }
        #endregion

        #region Piece
        public DelegateCommand DisplayPieceCommand { get; set; }
        public DelegateCommand DisplayCreatePieceCommand { get; set; }
        public DelegateCommand DisplayUpdatePieceCommand { get; set; }
        public DelegateCommand DeletePieceCommand { get; set; }
        public DelegateCommand ConfirmPieceCommand { get; set; }
        #endregion


        #endregion

        public DelegateCommand DisplayLChartCommand { get; set; }


        //CRUD Composer

        public ViewModelMain()
        {

            DisplayLChartCommand = new DelegateCommand(DisplayLChart);
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

            //Type
            DisplayTypeCommand = new DelegateCommand(DisplayTypeStartup);
            DisplayCreateTypeCommand = new DelegateCommand(DisplayCreateType);
            DisplayUpdateTypeCommand = new DelegateCommand(DisplayUpdateType);
            DeleteTypeCommand = new DelegateCommand(DeleteType);

            //Piece
            DisplayPieceCommand = new DelegateCommand(DisplayPieceStartup);
            DisplayCreatePieceCommand = new DelegateCommand(DisplayCreatePiece);
            DisplayUpdatePieceCommand = new DelegateCommand(DisplayUpdatePiece);
            DeletePieceCommand = new DelegateCommand(DeletePiece);

            _viewModel2 = new ViewModelLChart();
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

        public ViewModelBase ViewLiveChart
        {
            get => _viewModel2;
            set
            {
                _viewModel2 = value;
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

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();

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

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();

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

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();

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

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();
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

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();
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

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                }

            }
            else
            {
                MessageBox.Show("Please select a artist that you want to remove");
            }

        }
        #endregion

        #region CRUD Piece
        //CRUD Piece
        private void DisplayCreatePiece()
        {
            var listComposers = ((ViewModelPieceData)this.ViewModel).ListComposers;
            var listTypes = ((ViewModelPieceData)this.ViewModel).ListTypes;

            this.ViewModel = new ViewModelCreateUpdatePiece(listComposers, listTypes);
            ConfirmPieceCommand = new DelegateCommand(CreatePiece);
        }

        private void DisplayUpdatePiece()
        {
            var selectedPiece = ((AbstractViewModelContainer)this.ViewModel).SelectedPiece;
            var listComposers = ((ViewModelPieceData)this.ViewModel).ListComposers;
            var listTypes = ((ViewModelPieceData)this.ViewModel).ListTypes;


            if (selectedPiece is not null)
            {
                this.ViewModel = new ViewModelCreateUpdatePiece(selectedPiece, listComposers, listTypes);
                ConfirmPieceCommand = new DelegateCommand(UpdatePiece);
            }
            else
            {
                MessageBox.Show("Please select an artist that you want to update");
            }
        }

        private void CreatePiece()
        {
            var newPiece = ((ViewModelCreateUpdatePiece)this.ViewModel).Piece;
            newPiece.ComposerID = newPiece.Composer.ID;
            newPiece.TypeID = newPiece.Type.ID;


            newPiece.Composer = null;
            newPiece.Type = null;
            var result = (this.ViewModel as ViewModelCreateUpdatePiece).AddPieceMethode(newPiece);

            

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{newPiece.NamePiece } is added to DB");
                DisplayPieceStartup();

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();
            }
        }

        private async void UpdatePiece()
        {
            var selectedPiece = ((ViewModelCreateUpdatePiece)this.ViewModel).Piece;

            selectedPiece.ComposerID = selectedPiece.Composer.ID;
            selectedPiece.TypeID = selectedPiece.Type.ID;


            selectedPiece.Composer = null;
            selectedPiece.Type = null;

            var result = ((ViewModelCreateUpdatePiece)this.ViewModel).UpdatePieceMethode(selectedPiece);

            if (result == null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{selectedPiece.NamePiece } is updated to DB");
                DisplayPieceStartup();

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();
            }

        }

        private async void DeletePiece()
        {
            var selectedPiece = ((AbstractViewModelContainer)this.ViewModel).SelectedPiece;

            if (selectedPiece is not null)
            {
                if (MessageBox.Show($"Are you sure you want to delete {selectedPiece.NamePiece}?",
                        "Delete artist",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string result = null;

                    using (ApiCall ac = new ApiCall())
                    {
                        result = await ac.DeletePiece(selectedPiece);
                    }

                    MessageBox.Show($"{selectedPiece.NamePiece} is deleted from DB");
                    DisplayPieceStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                }

            }
            else
            {
                MessageBox.Show("Please select a artist that you want to remove");
            }

        }
        #endregion

        #region CRUD Type
        //CRUD Type
        private void DisplayCreateType()
        {
            this.ViewModel = new ViewModelCreateUpdateType();
            ConfirmTypeCommand = new DelegateCommand(CreateType);
        }

        private void DisplayUpdateType()
        {
            var selectedType = ((AbstractViewModelContainer)this.ViewModel).SelectedType;
            if (selectedType is not null)
            {
                this.ViewModel = new ViewModelCreateUpdateType(selectedType);
                ConfirmTypeCommand = new DelegateCommand(UpdateType);
            }
            else
            {
                MessageBox.Show("Please select an artist that you want to update");
            }


        }

        private void CreateType()
        {
            var newType = ((ViewModelCreateUpdateType)this.ViewModel).Type;
            var result = (this.ViewModel as ViewModelCreateUpdateType).AddTypeMethode(newType);

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{newType.NameType } is added to DB");
                DisplayTypeStartup();

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();
            }
        }

        private async void UpdateType()
        {
            var selectedType = ((ViewModelCreateUpdateType)this.ViewModel).Type;

            var result = ((ViewModelCreateUpdateType)this.ViewModel).UpdateTypeMethode(selectedType);

            if (result is null)
            {
                MessageBox.Show("Something went wrong");
            }
            else
            {
                MessageBox.Show($"{selectedType.NameType } is updated to DB");
                DisplayTypeStartup();

                //Update LiveChart
                _viewModel2 = new ViewModelLChart();
            }

        }

        private async void DeleteType()
        {
            var selectedType = ((AbstractViewModelContainer)this.ViewModel).SelectedType;

            if (selectedType is not null)
            {
                if (MessageBox.Show($"Are you sure you want to delete {selectedType.NameType}?",
                        "Delete artist",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string result = null;

                    using (ApiCall ac = new ApiCall())
                    {
                        result = await ac.DeleteType(selectedType);
                    }

                    MessageBox.Show($"{selectedType.NameType} is deleted from DB");
                    DisplayTypeStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
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
