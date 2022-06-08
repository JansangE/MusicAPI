using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;

namespace EP.MAINAPP.ViewModels.Piece
{
    public class ViewModelCreateUpdatePiece : AbstractViewModelContainer
    {
        private int _idOldType = 0;

        public int IdOldType
        {
            get => _idOldType;
            set
            {
                _idOldType = value;
                OnPropertyChanged();
            }
        }

        public ViewModelCreateUpdatePiece(ObservableCollection<DOMAIN.Composer> listComposers, ObservableCollection<DOMAIN.Type> listTypes) : base()
        {
            Piece = new DOMAIN.Piece();
            Composer = new DOMAIN.Composer();
            Type = new DOMAIN.Type();

            ListComposers = listComposers;
            ListTypes = listTypes;


        }

        public ViewModelCreateUpdatePiece(DOMAIN.Piece piece, ObservableCollection<DOMAIN.Composer> listComposers, ObservableCollection<DOMAIN.Type> listTypes) : base()
        {
            GetPiece(piece);

            ListComposers = listComposers;
            ListTypes = listTypes;

            //for (int i = 0; i < ListTypes.Count; i++)
            //{
            //    if (ListTypes[i].ID == piece.Type.ID)
            //    {
            //        IdOldType = i;
            //    }
            //}

            //OnPropertyChanged(nameof(IdOldType));
        }


        //Methode
        private async void GetPiece(DOMAIN.Piece piece)
        {
            using (ApiCall ac = new ApiCall())
            {
                Piece = await ac.GetSelectedPiece(piece);
            }
        }

        public async Task<string> AddPieceMethode(DOMAIN.Piece piece)
        {
            string result = null;

            if (AllRequired(piece))
            {
                using (ApiCall ac = new ApiCall())
                {
                    piece = await ac.AddPiece(piece);
                    OnPropertyChanged(nameof(ListPieces));
                }

                if (piece is not null)
                {
                    result = $"{piece.NamePiece} is added in da DB";
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

        public async Task<string> UpdatePieceMethode(DOMAIN.Piece piece)
        {
            string result = null;

            if (AllRequired(piece))
            {
                using (ApiCall ac = new ApiCall())
                {
                    piece = await ac.UpdatePiece(piece);
                    OnPropertyChanged(nameof(ListPieces));
                }

                if (piece is not null)
                {
                    result = $"{piece.NamePiece} is updated in da DB";
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

        private bool AllRequired(DOMAIN.Piece piece)
        {
            return true;
        }
    }
}
