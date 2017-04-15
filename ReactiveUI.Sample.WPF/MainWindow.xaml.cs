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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;

namespace ReactiveUI.Sample.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ReactiveList<Datum> DataSource = new ReactiveList<Datum>();
        IReactiveDerivedList<Datum> Data { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            for (int i = 0; i < 1000; i++)
                DataSource.Add(new Datum());

            Task.Run(()=>
                Data = DataSource.CreateDerivedCollection(x => x, Filter, scheduler: RxApp.TaskpoolScheduler)
            );
        }


        void Reset()
        {
            
            for (int i = 200; i < 250; i++)
                DataSource[i].Show = true;


            DataSource.Add(new Datum());
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Reset();
           // Data.Reset();
            
        }

        public bool Filter(Datum item)
        {
            return item.Show;
        }

        public class Datum
        {
            public bool Show { get; set; }
        }
    }
}
