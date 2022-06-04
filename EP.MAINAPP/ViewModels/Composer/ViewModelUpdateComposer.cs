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
    public class ViewModelUpdateComposer : ViewModelContainer
    {
        private ICommand _addComposerCommand;
        public DelegateCommand TestCommand { get; set; }
        public ViewModelUpdateComposer()
        {
            //AddComposerCommand = new RelayCommand((param) => AddComposerMethode(param));
            //TestCommand = new DelegateCommand(TestMethode);

            Composer = new DOMAIN.Composer();
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
        private void TestMethode()
        {
            string test;
            MessageBox.Show($"{this.Composer.FirstName}");
            
        }

        public async Task<string> AddComposerMethode(DOMAIN.Composer composer)
        {
            string result = null;

            if (AllRequired(composer))
            {
                using (ApiCall ac = new ApiCall())
                {
                    composer = await ac.AddComposer(composer);
                    OnPropertyChanged(nameof(ListComposers));
                }

                if (composer is not null)
                {
                    result = $"{composer.FirstName} is added in da DB";
                }
                else
                {
                    result = $"Something went wrong";
                }
            }

            return result;

        }

        private bool AllRequired(DOMAIN.Composer composer)
        {
            return true;
        }
    }
}
