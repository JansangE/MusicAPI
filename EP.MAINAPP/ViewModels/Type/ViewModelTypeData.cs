using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EP.MAINAPP.ViewModels.Type
{
    public class ViewModelTypeData : AbstractViewModelContainer
    {
        public ViewModelTypeData() : base()
        {
            IsReady = Visibility.Hidden;
            OnPropertyChanged(nameof(IsReady));

            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListTypes = await ac.GetAllTypes();
                OnPropertyChanged(nameof(ListTypes));
            }

            IsReady = Visibility.Visible;
            OnPropertyChanged(nameof(IsReady));
        }
    }
}
