using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.XamForms;
using ReactiveUI.XamlForms.Sample.ViewModels;
using Xamarin.Forms;

namespace ReactiveUI.XamlForms.Sample
{
	public partial class MainPage : ReactiveContentPage<MainPageViewModel>
    {
		public MainPage()
		{
			InitializeComponent();
            
			this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Text);
			this.OneWayBind(ViewModel, vm => vm.NavigateToSecondPage, v => v.btn.Command);
		}
	}
}
