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
using ReactiveUI.XamlForms.Sample;
using ReactiveUI.XamlForms.Sample.ViewModels;
using System.Reactive.Linq;

namespace ReactiveUI.Sample.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppBootstrapper _bootStrapper;
        AppBootstrapper BootStrapper
        {
            get
            {
                if (_bootStrapper == null)
                    _bootStrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();

                return _bootStrapper;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            RxApp.SuspensionHost.ObserveAppState<AppBootstrapper>()
                     .Where(data => data != null)
                     .ObserveOn(RxApp.MainThreadScheduler)
                     .Take(1)
                     .Subscribe(_ =>
                     {
                         Content = BootStrapper.CreateMainPage();
                     });


         
        }
    }
}
