using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;

namespace EP.MAINAPP.ViewModels.Composer
{
    public class ViewModelComposerData : ViewModelContainer
    {
        
        public ViewModelComposerData() : base()
        {
            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListComposers = await ac.GetAllComposer();
                OnPropertyChanged(nameof(ListComposers));
            }
        }
    }
}
