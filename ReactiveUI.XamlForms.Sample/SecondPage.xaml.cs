using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
			this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Text);
			this.BindCommand(ViewModel, vm => vm.NavigateBack, v => v.btn);
            this.OneWayBind(ViewModel, vm => vm, v => v.gridView.ViewModel);
		}

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            //if(propertyName == "ViewModel")
            //{
            //    gridView.ViewModel = ViewModel;
            //    gridView.BindingContext = ViewModel;
            //}
        }
    }
}
