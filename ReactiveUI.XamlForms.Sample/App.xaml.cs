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
        CompositionRoot _compositionRoot;

        ApplicationState _bootStrapper;
        ApplicationState BootStrapper
		{
			get
			{
				if (_bootStrapper == null)
					_bootStrapper = RxApp.SuspensionHost.GetAppState<ApplicationState>();

				return _bootStrapper;
			}
		}

		public App()
		{
			InitializeComponent();
            _compositionRoot = new CompositionRoot();
            MainPage = _compositionRoot.CreateMainPage();
        }
		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}


		static AutoSuspendHelper autoSuspendHelper;
		public static AutoSuspendHelper Initialize(

#if __ANDROID__
			Android.App.Application application
#endif

#if __UNIFIED__
			UIKit.UIApplicationDelegate application
#endif

#if WINDOWS_UWP
            Windows.UI.Xaml.Application application,
            IActivatedEventArgs e
#endif

			)
		{

            // this mainly happens with android where activity is destroyed
            // but everything else stays in memory
            if(autoSuspendHelper != null)
            {
                return autoSuspendHelper;
            }

			RxApp.SuspensionHost.CreateNewAppState = () =>
			{
                //Ensure App has initialize
                //Xam Forms gets annoyed if you don't have a page on the
                //Navigation stack so this just ensures that it is there
				var bootStrapper = new ApplicationState();
				bootStrapper.Init();
				return bootStrapper;
			};

			//Make sure this is called after something accesses RxApp so that RxApp can register its defaults
			//And then this can override those
			 Akavache.Mobile.Registrations.Register(Locator.CurrentMutable);

            autoSuspendHelper = new AutoSuspendHelper(application);
#if WINDOWS_UWP
            autoSuspendHelper.OnLaunched(e);
#endif

            RxApp.SuspensionHost.SetupDefaultSuspendResume();
			return autoSuspendHelper;
		}


	}
}
