using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using Xamarin.Forms;
#if WINDOWS_UWP
using Windows.ApplicationModel.Activation;
#endif

namespace ReactiveUI.XamlForms.Sample
{
	public partial class App : Application
	{
        static AppBootstrapper _bootStrapper;
        AppBootstrapper BootStrapper
        {
            get
            {
				if (_bootStrapper == null)
					_bootStrapper = RxApp.SuspensionHost.GetAppState<AppBootstrapper>();
				
                return _bootStrapper;
            }
        }

        public App ()
		{
			InitializeComponent();
			MainPage = BootStrapper.CreateMainPage();
		}
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}


        static AutoSuspendHelper autoSuspendHelper;
		public static void Initialize(

#if __ANDROID__
			Android.App.Application application
#endif

#if WINDOWS_UWP
            Windows.UI.Xaml.Application application,
            IActivatedEventArgs e
#endif

			)
		{
			RxApp.SuspensionHost.CreateNewAppState = () =>
			{
				_bootStrapper = new AppBootstrapper();
				_bootStrapper.Init();
				return _bootStrapper;
			};

			//Make sure this is called after something accesses RxApp so that RxApp can register its defaults
			//And then this can override those
			 Akavache.Mobile.Registrations.Register(Locator.CurrentMutable);

            autoSuspendHelper = new AutoSuspendHelper(application);
#if WINDOWS_UWP
            autoSuspendHelper.OnLaunched(e);
#endif


            RxApp.SuspensionHost.SetupDefaultSuspendResume();
		}
	}
}
