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
	public partial class SecondPage : ReactiveContentPage<SecondPageViewModel>
	{
		public SecondPage ()
		{
			InitializeComponent ();
			//Reactiveui doesn't set the bindingcontext
			//so if you want to use bindingcontext then set that yourself
			this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Text);
			this.OneWayBind(ViewModel, vm => vm.NavigateBack, v => v.btn.Command);
		}
	}
}
