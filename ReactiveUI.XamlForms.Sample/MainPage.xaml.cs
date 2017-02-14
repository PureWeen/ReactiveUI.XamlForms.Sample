using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI.XamForms;
using ReactiveUI.XamlForms.Sample.ViewModels;
using Xamarin.Forms;
using Splat;
using System.Diagnostics;

namespace ReactiveUI.XamlForms.Sample
{
	public partial class MainPage : ReactiveContentPage<MainPageViewModel>
    {
		public MainPage()
		{
			InitializeComponent();

			//Reactiveui doesn't set the bindingcontext
			//so if you want to use bindingcontext then set that yourself
			this.OneWayBind(ViewModel, vm => vm.MainText, v => v.lbl.Text);
			this.OneWayBind(ViewModel, vm => vm.NavigateToSecondPage, v => v.btn.Command);
		}


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == nameof(ViewModel))
            {
                Debug.WriteLine($"ViewModel Set: {ViewModel}");
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            
        }
    }
}
