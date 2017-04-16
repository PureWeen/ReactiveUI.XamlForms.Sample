using ReactiveUI.XamlForms.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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

namespace ReactiveUI.XamlForms.Sample
{
    /// <summary>
    /// Interaction logic for SecondPage.xaml
    /// </summary>
    public partial class SecondPage : UserControl, IViewFor<SecondPageViewModel>
    {
        public SecondPage()
        {
            InitializeComponent();
            this.WhenActivated((CompositeDisposable d) =>
            {

                this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Content).DisposeWith(d);
                this.BindCommand(ViewModel, vm => vm.NavigateBack, v => v.btn).DisposeWith(d);

                //this.OneWayBind(ViewModel, vm => vm.GridViewItems, v => v.gridView.ItemsSource);
            });
        }


        public SecondPageViewModel ViewModel
        {
            get { return (SecondPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); DataContext = value; }
        }


        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(SecondPageViewModel),
                typeof(SecondPage));


        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (SecondPageViewModel)value; }
        }
    }
}
