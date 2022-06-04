using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;

namespace EP.MAINAPP.ViewModels.Composer
{
    public class ViewModelCreateUpdateComposer : ViewModelContainer
    {
        private ICommand _addComposerCommand;
        public ViewModelCreateUpdateComposer()
        {
            AddComposerCommand = new RelayCommand((param) => AddComposerMethode(param));
        }

        public ICommand AddComposerCommand
        {
            get => _addComposerCommand;
            set
            {
                _addComposerCommand = value;
                OnPropertyChanged();
            }
        }

        private async void AddComposerMethode(object param)
        {
            var listText = (ObservableCollection<object>)param;

            if (AllRequired(listText))
            {
                DOMAIN.Composer newComposer = new DOMAIN.Composer()
                {
                    FirstName = listText[0].ToString(),
                    LastName = listText[1].ToString(),
                    Birtday = Convert.ToDateTime(listText[2].ToString())
                };

                using (ApiCall ac = new ApiCall())
                {
                    newComposer = await ac.AddComposer(newComposer);
                    OnPropertyChanged(nameof(ListComposers));
                }

                if (newComposer is not null)
                {
                    MessageBox.Show($"{newComposer.NickName} is added");
                }
                else
                {
                    MessageBox.Show("Something went wrong add Composer");
                }
            }


            //var composer = new DOMAIN.Composer()
            //{
            //    FirstName = "testFirstname",
            //    LastName = "testLastname",
            //    Birtday = DateTime.Now
            //};

           
            


            ///Okie
        }

        private bool AllRequired(ObservableCollection<object> listText)
        {
            return true;
        }
    }
}
