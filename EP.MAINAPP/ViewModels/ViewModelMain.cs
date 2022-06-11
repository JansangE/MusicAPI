using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using EP.DOMAIN;
using EP.MAINAPP.ViewModels.Artist;
using EP.MAINAPP.ViewModels.Composer;
using EP.MAINAPP.ViewModels.LChart;
using EP.MAINAPP.ViewModels.PDF;
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

        public DelegateCommand DownloadPDFCommand { get; set; }

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

        public DelegateCommand ExitCommand { get; set; }


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

            ExitCommand = new DelegateCommand(ExitMethode);

            _viewModel2 = new ViewModelLChart();
            
            DisplayPieceStartup();


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

        private void ExitMethode()
        {
            if (MessageBox.Show("Do you want to leave this beautiful application?", "And you sure", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }

        }


        private void DisplayArtistStartup()
        {

            this.ViewModel = new ViewModelArtistData();
            DownloadPDFCommand = new DelegateCommand(DownloadPDF);
        }

        private void DisplayComposerStartup()
        {
            this.ViewModel = new ViewModelComposerData();
            DownloadPDFCommand = new DelegateCommand(DownloadPDF);
        }

        private void DisplayLChart()
        {
            this.ViewModel = new ViewModelLChart();
        }

        private void DisplayPieceStartup()
        {
            this.ViewModel = new ViewModelPieceData();
            DownloadPDFCommand = new DelegateCommand(DownloadPDF);

        }

        private void DisplayTypeStartup()
        {
            this.ViewModel = new ViewModelTypeData();
            DownloadPDFCommand = new DelegateCommand(DownloadPDF);
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
            if (!AllRequired(this.ViewModel, newComposer))
            {
                MessageBox.Show("Please complete all the field");
            }
            else
            {
                var result = (this.ViewModel as ViewModelCreateUpdateComposer).AddComposerMethode(newComposer);

                if (result is null)
                {
                    MessageBox.Show("Something went wrong");
                }
                else
                {
                    MessageBox.Show($"{newComposer.FirstName }  {newComposer.LastName} is added to DB");
                    DisplayComposerStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                    OnPropertyChanged(nameof(ViewLiveChart));


                }
            }
        }

        private async void UpdateComposer()
        {
            var selectedComposer = ((ViewModelCreateUpdateComposer)this.ViewModel).Composer;

            if (!AllRequired(this.ViewModel, selectedComposer))
            {
                MessageBox.Show("Please complete all the field");
            }
            else
            {
                var result = ((ViewModelCreateUpdateComposer)this.ViewModel).UpdateComposerMethode(selectedComposer);

                if (result is null)
                {
                    MessageBox.Show("Something went wrong");
                }
                else
                {
                    MessageBox.Show($"{selectedComposer.FirstName }  {selectedComposer.LastName} is updated to DB");
                    DisplayComposerStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
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

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                    OnPropertyChanged(nameof(ViewLiveChart));
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

            if (!AllRequired(this.ViewModel, newArtist))
            {
                MessageBox.Show("Please fill all the required field");
            }
            else
            {
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
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
            }

           
        }

        private async void UpdateArtist()
        {
            var selectedArtist = ((ViewModelCreateUpdateArtist)this.ViewModel).Artist;

            if (!AllRequired(this.ViewModel, selectedArtist))
            {
                MessageBox.Show("Please fill all the required field");
            }
            else
            {
                var result = ((ViewModelCreateUpdateArtist)this.ViewModel).UpdateArtistMethode(selectedArtist);

                if (result is null)
                {
                    MessageBox.Show("Something went wrong");
                }
                else
                {
                    MessageBox.Show($"{selectedArtist.NameArtist} is updated to DB");
                    DisplayArtistStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
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
                    OnPropertyChanged(nameof(ViewLiveChart));
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

            if (!AllRequired(this.ViewModel, newPiece))
            {
                MessageBox.Show("Please fill all the required fields");
            }
            else
            {
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
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
            }
            
        }

        private async void UpdatePiece()
        {
            var selectedPiece = ((ViewModelCreateUpdatePiece)this.ViewModel).Piece;

            if (!AllRequired(this.ViewModel, selectedPiece))
            {
                MessageBox.Show("Please fill all the required fields");
            }
            else
            {
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
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
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
                    OnPropertyChanged(nameof(ViewLiveChart));
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

            if (!AllRequired(this.ViewModel, newType))
            {
                MessageBox.Show("Please fill all the required field");
            }
            else
            {

                var result = (this.ViewModel as ViewModelCreateUpdateType).AddTypeMethode(newType);

                if (result is null)
                {
                    MessageBox.Show("Something went wrong");
                }
                else
                {
                    MessageBox.Show($"{newType.NameType} is added to DB");
                    DisplayTypeStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
            }
        }

        private async void UpdateType()
        {
            var selectedType = ((ViewModelCreateUpdateType)this.ViewModel).Type;

            if (!AllRequired(this.ViewModel, selectedType))
            {
                MessageBox.Show("Please fill all the required field");
            }
            else
            {

                var result = ((ViewModelCreateUpdateType)this.ViewModel).UpdateTypeMethode(selectedType);

                if (result is null)
                {
                    MessageBox.Show("Something went wrong");
                }
                else
                {
                    MessageBox.Show($"{selectedType.NameType} is updated to DB");
                    DisplayTypeStartup();

                    //Update LiveChart
                    _viewModel2 = new ViewModelLChart();
                    OnPropertyChanged(nameof(ViewLiveChart));
                }
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
                    OnPropertyChanged(nameof(ViewLiveChart));
                }

            }
            else
            {
                MessageBox.Show("Please select a artist that you want to remove");
            }

        }
        #endregion

        private bool AllRequired(ViewModelBase viewModel, object item)
        {
            if (viewModel is ViewModelCreateUpdateArtist)
            {
                var tempItem = ((ViewModelCreateUpdateArtist)viewModel).Artist;
                return tempItem.NameArtist != null && tempItem.NameArtist != string.Empty;
            }
            else if (viewModel is ViewModelCreateUpdateComposer)
            {
                var tempItem = ((ViewModelCreateUpdateComposer)viewModel).Composer;
                return tempItem.FirstName != null && tempItem.FirstName != string.Empty &&
                       tempItem.LastName != null && tempItem.LastName != string.Empty &&
                       tempItem.Birthday != null;
            }
            else if (viewModel is ViewModelCreateUpdatePiece)
            {
                var tempItem = ((ViewModelCreateUpdatePiece)viewModel).Piece;
                return tempItem.NamePiece != null && tempItem.NamePiece != string.Empty &&
                       tempItem.Composer != null && tempItem.Type != null;
            }
            else
            {
                var tempItem = ((ViewModelCreateUpdateType)viewModel).Type;
                return tempItem.NameType != null && tempItem.NameType != string.Empty;
            }

            return false;
        }
        private void DownloadPDF()
        {
            if (this.ViewModel is ViewModelArtistData)
            {
                var list = ((ViewModelArtistData)this.ViewModel).ListArtists;

                var result = new ViewModelPDF(list);
            }
            else if (this.ViewModel is ViewModelComposerData)
            {
                var list = ((ViewModelComposerData)this.ViewModel).ListComposers;

                var result = new ViewModelPDF(list);
            }
            else if (this.ViewModel is ViewModelPieceData)
            {
                var list = ((ViewModelPieceData)this.ViewModel).ListPieces;

                var result = new ViewModelPDF(list);
            }
            else if (this.ViewModel is ViewModelTypeData)
            {
                var list = ((ViewModelTypeData)this.ViewModel).ListTypes;

                var result = new ViewModelPDF(list);
            }
        }

    }
}
