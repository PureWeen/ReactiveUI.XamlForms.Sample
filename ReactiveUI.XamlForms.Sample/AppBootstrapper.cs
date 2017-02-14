using System;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using ReactiveUI.XamForms;
using Akavache;
using System.Runtime.Serialization;
using System.Linq;
using ReactiveUI.XamlForms.Sample.ViewModels;
using System.Reactive.Linq;
using System.Threading.Tasks;

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

        //The init is only called from CreateNewAppState... If this comes from the CACHE
        //then the Router is already instantiated and set to the correct VM
        //I await the navigation because Xam Forms can get annoyed if there's not a 
        //Page on the stack once it starts up
        public async Task Init()
        {
            Router = new RoutingState();
            
            Router.NavigationStack.Add(new MainPageViewModel(this));
            Router.NavigationStack.Add(new SecondPageViewModel(this));
            //await Router.Navigate.Execute(new MainPageViewModel(this));
        }

        public Page CreateMainPage()
        {
            var viewHost =  new ReactiveUI.XamForms.RoutedViewHost();


            return viewHost;
        }
    }
}
