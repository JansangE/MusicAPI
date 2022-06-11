using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace EP.MAINAPP.ViewModels.LChart
{
    public class ViewModelLChart : AbstractViewModelContainer
    {
        private SeriesCollection _seriesCollection;
        

        public SeriesCollection SeriesCollection
        {
            get
            {
                return _seriesCollection;
            }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
              
            }
        }
        public ViewModelLChart()
        {
            Init();
        }

        private async void Init()
        {
            ObservableCollection<int> list = new ObservableCollection<int>();
            using (ApiCall ac = new ApiCall())
            {
                list = await ac.GetListCountDatabase();
            }



            _seriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Artist",
                    Values = new ChartValues<int> {list.ElementAt(0)},
                    DataLabels = true,
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#036578")
                },
                new PieSeries
                {
                    Title = "Composer",
                    Values = new ChartValues<int> {list.ElementAt(1)},
                    DataLabels = true,
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#00cf80")
                },
                new PieSeries
                {
                    Title = "Piece",
                    Values = new ChartValues<int> {list.ElementAt(2)},
                    DataLabels = true,
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffd700")
                },
                new PieSeries
                {
                    Title = "Type",
                    Values = new ChartValues<int> {list.ElementAt(3)},
                    DataLabels = true,
                    Fill = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF3636")
                }
            };

            
            OnPropertyChanged(nameof(SeriesCollection));
            
        }

    }

}
