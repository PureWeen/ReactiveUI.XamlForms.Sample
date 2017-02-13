using System;
using System.Collections.Generic;
using System.Text;
using Akavache;
using System.Reactive;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.XamlForms.Sample.ViewModels
{
    [DataContract]
    public class MainPageViewModel : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {
        [IgnoreDataMember]
        public string UrlPathSegment
        {
            get { return "Main Page"; }
        }

        [IgnoreDataMember]
        public IScreen HostScreen { get; protected set; }


		public string MainText { get { return "Main Page"; } }

        public ViewModelActivator Activator
        {
            get;
            private set;
        }


		public ReactiveCommand<Unit, IRoutableViewModel> NavigateToSecondPage { get; private set;}


        public MainPageViewModel(IScreen screen = null)
        {
            Activator = new ViewModelActivator();

            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            NavigateToSecondPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new SecondPageViewModel(screen)));
				                                                         
        }
    }

}
