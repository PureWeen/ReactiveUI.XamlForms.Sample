using System;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using ReactiveUI.XamForms;
using Akavache;
using System.Runtime.Serialization;
using System.Linq;
using ReactiveUI.XamlForms.Sample.ViewModels;

namespace ReactiveUI.XamlForms.Sample
{
	[DataContract]
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        // The Router holds the ViewModels for the back stack. Because it's
        // in this object, it will be serialized automatically.


        public RoutingState _Router;
        [DataMember]
        public RoutingState Router
        {
            get
            {
                return _Router;
            }
            set
            {
                _Router = value;
            }
        }


        public AppBootstrapper()
        {
			Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
			BlobCache.ApplicationName = "ReactiveUI.XamlForms.Sample";
			Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainPageViewModel>));
			Locator.CurrentMutable.Register(() => new SecondPage(), typeof(IViewFor<SecondPageViewModel>));
        }

        public void Init()
        { 
			Router = new RoutingState();
			Router.Navigate.Execute(new MainPageViewModel(this));
        }

        public Page CreateMainPage()
        {
            // NB: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrapper as an IScreen.
            return new ReactiveUI.XamForms.RoutedViewHost();
        }
    }
}
