using ReactiveUI.XamForms;
using ReactiveUI.XamlForms.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace ReactiveUI.XamlForms.Sample
{
	public partial class GridView : ReactiveUI.XamForms.ReactiveContentView<SecondPageViewModel>
    {
		public GridView ()
		{
			InitializeComponent ();            

            
            
		}


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}
