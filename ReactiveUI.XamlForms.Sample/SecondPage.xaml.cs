using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
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

            this.WhenActivated(disp =>
            {
                this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Text)
                    .DisposeWith(disp);
                this.BindCommand(ViewModel, vm => vm.NavigateBack, v => v.btn)
                    .DisposeWith(disp);
                this.OneWayBind(ViewModel, vm => vm, v => v.gridView.ViewModel)
                    .DisposeWith(disp);
            });
        }
    }
}
