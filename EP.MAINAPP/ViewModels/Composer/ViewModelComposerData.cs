using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EP.DOMAIN;

namespace EP.MAINAPP.ViewModels.Composer
{
    public class ViewModelComposerData : ViewModelBase
    {
        private ObservableCollection<DOMAIN.Composer> listComposers;
        

        public ViewModelComposerData()
        {
            listComposers = new ObservableCollection<DOMAIN.Composer>();
            Init();
        }

        public ObservableCollection<DOMAIN.Composer> ListComposers
        {
            get => listComposers;
            set
            {
                listComposers = value;
                OnPropertyChanged();
            }
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall(Url: "http://localhost:5106"))
            {
                listComposers = await ac.GetAllComposer();
                OnPropertyChanged(nameof(ListComposers));
            }

        }
    }
}
