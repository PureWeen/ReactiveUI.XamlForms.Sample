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
using System.Reactive.Disposables;
using ReactiveUI.XamlForms.Sample.ViewModels;

namespace ReactiveUI.XamlForms.Sample
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl, IViewFor<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
            
            this.WhenActivated((CompositeDisposable d) =>
            {

                this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Content)
                    .DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.NavigateToSecondPage, v => v.btn.Command)
                    .DisposeWith(d);
            });
        }

        public MainPageViewModel ViewModel
        {
            get { return (MainPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); DataContext = value; }
        }


        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(MainPageViewModel),
                typeof(MainPage));


        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainPageViewModel)value; }
        }

    }
}
