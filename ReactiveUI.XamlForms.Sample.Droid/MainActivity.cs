using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ReactiveUI.XamlForms.Sample.Droid
{
	[Activity (Label = "ReactiveUI.XamlForms.Sample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		public MainActivity()
		{

		}



		protected override void OnCreate (Bundle bundle)
		{
			//Make sure this is before OnCreate otherwise the life cycle OnCreate won't fire
			//that creates the app state
			ReactiveUI.XamlForms.Sample.App.Initialize(this.Application);

			base.OnCreate (bundle);

            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new ReactiveUI.XamlForms.Sample.App ());
		}
    }
}

