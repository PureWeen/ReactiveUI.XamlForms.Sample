using ReactiveUI.XamForms;
using ReactiveUI.XamlForms.Sample;
using ReactiveUI.XamlForms.Sample.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xamarin.Forms;

namespace ReactiveUI.XamlForms.Sample
{
    public class CompositionRoot
    {
        public CompositionRoot()
        {
            Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainPageViewModel>));
            Locator.CurrentMutable.Register(() => new SecondPage(), typeof(IViewFor<SecondPageViewModel>));
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
        }


#if !NET_45
        public Page CreateMainPage()
        {
            var mainPage = (ContentPage)Locator.Current.GetService<IViewFor<MainPageViewModel>>();
            var navPage =  new SampleRoutedViewHost(mainPage);


            var loadAppState =
                RxApp
                    .SuspensionHost
                    .ObserveAppState<ApplicationState>()
                    .Where(data => data != null)
                    .Take(1);

            // todo make better
            navPage.Events()
                .Appearing.Take(1)
                .SelectMany(_ => loadAppState)
                .ObserveOn(RxApp.MainThreadScheduler)
                .SelectMany(appState =>
                {
                    navPage.Router = appState.Router;
                    Locator.CurrentMutable.RegisterConstant<ISampleScreen>(appState);
                    return appState.Router.Load(() => new MainPageViewModel()).Take(1);
                })
                .Subscribe();

            return navPage;
        }
#else
        public RoutedViewHost CreateMainPage()
        {
            var host = new ReactiveUI.RoutedViewHost();

            if (Router == null)
                throw new ArgumentNullException(nameof(Router));

            host.Router = Router;
            return host;
        }
#endif
    }
}
