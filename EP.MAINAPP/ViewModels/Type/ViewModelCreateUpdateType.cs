using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;

namespace EP.MAINAPP.ViewModels.Type
{
    public class ViewModelCreateUpdateType : AbstractViewModelContainer
    {
        public ViewModelCreateUpdateType()
        {
            Type = new DOMAIN.Type();
        }

        public ViewModelCreateUpdateType(DOMAIN.Type type)
        {
            GetType(type);
        }

        private async void GetType(DOMAIN.Type type)
        {
            using (ApiCall ac = new ApiCall())
            {
                Type = await ac.GetSelectedType(type);
            }

        }

        public async Task<string> AddTypeMethode(DOMAIN.Type type)
        {
            string result = null;

            if (AllRequired(type))
            {
                using (ApiCall ac = new ApiCall())
                {
                    type = await ac.AddType(type);
                    OnPropertyChanged(nameof(ListTypes));
                }

                if (type is not null)
                {
                    result = $"{type.NameType} is added in da DB";
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

        public async Task<string> UpdateTypeMethode(DOMAIN.Type type)
        {
            string result = null;

            if (AllRequired(type))
            {
                using (ApiCall ac = new ApiCall())
                {
                    type = await ac.UpdateType(type);
                    OnPropertyChanged(nameof(ListTypes));
                }

                if (type is not null)
                {
                    result = $"{type.NameType} is updated in da DB";
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

        private bool AllRequired(DOMAIN.Type type)
        {
            return true;
        }
    }
}
