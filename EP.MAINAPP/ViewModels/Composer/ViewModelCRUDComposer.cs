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
    public class ViewModelCRUDComposer : AbstractViewModelContainer
    {
        public ViewModelCRUDComposer()
        {
            Composer = new DOMAIN.Composer();
        }

        public ViewModelCRUDComposer(DOMAIN.Composer composer)
        {
            GetComposer(composer);
        }

        private async void GetComposer(DOMAIN.Composer composer)
        {
            using (ApiCall ac = new ApiCall())
            {
                Composer = await ac.GetSelectedComposer(composer);
            }

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
                    result = $"Something went wrong with API";
                }
            }
            else
            {
                result = "Some field is not correct";
            }

            return result;
        }

        public async Task<string> UpdateComposerMethode(DOMAIN.Composer composer)
        {
            string result = null;

            if (AllRequired(composer))
            {
                using (ApiCall ac = new ApiCall())
                {
                    composer = await ac.UpdateComposer(composer);
                    OnPropertyChanged(nameof(ListComposers));
                }

                if (composer is not null)
                {
                    result = $"{composer.FirstName} is updated in da DB";
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

        public async Task<string> DeleteComposerMethode(DOMAIN.Composer composer)
        {
            string result = null;

            using (ApiCall ac = new ApiCall())
            {
                result = await ac.DeleteComposer(composer);
                OnPropertyChanged(nameof(ListComposers));
            }

            return result;
        }

        private bool AllRequired(DOMAIN.Composer composer)
        {
            return true;
        }
    }
}
