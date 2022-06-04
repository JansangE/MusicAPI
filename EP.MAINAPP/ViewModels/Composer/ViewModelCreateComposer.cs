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
    public class ViewModelCreateComposer : ViewModelContainer
    {
        public ViewModelCreateComposer()
        {
            Composer = new DOMAIN.Composer();
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

        private bool AllRequired(DOMAIN.Composer composer)
        {
            return true;
        }
    }
}
