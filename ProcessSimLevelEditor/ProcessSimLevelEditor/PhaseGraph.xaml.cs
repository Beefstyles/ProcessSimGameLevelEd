using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace ProcessSimLevelEditor
{
    /// <summary>
    /// Interaction logic for PhaseGraph.xaml
    /// </summary>
    public partial class PhaseGraph : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public PhaseGraph()
        {
            InitializeComponent();
            //returnPhase = (setPressure <= (0.5491 * Math.Pow(setTemp, 2) - 109.38 * setTemp + 5337.2)) ? 1 : 0;
            SeriesCollection = new SeriesCollection
            {
                /*
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                }
                /*new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
                */
            };

            List<double> ValList = new List<double>();
            

            for (int i = 70; i < 200; i++)
            {
                ValList.Add(i);
            }
            SeriesCollection.Add(new LineSeries
            {
                Title="Methane",
                Values = new ChartValues<double> { ValList.ToArray() },
                LineSmoothness = 0, //0: rect lines, 1: really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeround = Brushes.Gray
            });

            List<string> LabelArray = new List<string>();
            for (int i = 70; i < 200; i++)
            {
                LabelArray.Add(i.ToString());
            }
            Labels = LabelArray.ToArray();
            //Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: rect lines, 1: really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeround = Brushes.Gray
            });

            //modifying any series values will also animate and update the chart
            //SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
