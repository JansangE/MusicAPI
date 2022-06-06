using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.MAINAPP.ViewModels
{
    public abstract class AbstractViewModelContainer : ViewModelBase
    {
        private DOMAIN.Composer _composer;
        protected DOMAIN.Composer _SelectedComposer;

        public DOMAIN.Composer Composer
        {
            get => _composer;
            set
            {
                _composer = value;
                OnPropertyChanged();
            }
        }

        public DOMAIN.Composer SelectedComposer
        {
            get => _SelectedComposer;
            set
            {
                _SelectedComposer = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DOMAIN.Composer> ListComposers
        {
            get;
            set;
        }

      

        public AbstractViewModelContainer() : base()
        {
            //ListComposers = new ObservableCollection<DOMAIN.Composer>();
        }
    }
}
