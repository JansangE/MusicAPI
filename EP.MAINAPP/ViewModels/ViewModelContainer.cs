using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.MAINAPP.ViewModels
{
    public abstract class ViewModelContainer : ViewModelBase
    {
        public DOMAIN.Composer Composer
        {
            get => Composer;
            set
            {
                Composer = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DOMAIN.Composer> ListComposers
        {
            get;
            set;
        }

      

        public ViewModelContainer() : base()
        {
            //ListComposers = new ObservableCollection<DOMAIN.Composer>();
        }
    }
}
