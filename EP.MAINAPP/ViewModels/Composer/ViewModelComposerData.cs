﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EP.DOMAIN;

namespace EP.MAINAPP.ViewModels.Composer
{
    public class ViewModelComposerData : AbstractViewModelContainer
    {
        
        public ViewModelComposerData() : base()
        {
            IsReady = Visibility.Hidden;
            OnPropertyChanged(nameof(IsReady));
            Init();
        }

        private async Task Init()
        {
            using (ApiCall ac = new ApiCall())
            {
                ListComposers = await ac.GetAllComposer();
                OnPropertyChanged(nameof(ListComposers));
            }

            IsReady = Visibility.Visible;
            OnPropertyChanged(nameof(IsReady));
        }
    }
}
