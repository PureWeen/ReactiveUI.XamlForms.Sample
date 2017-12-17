using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.XamlForms.Sample.ViewModels
{
    [DataContract]
    public class MainPageViewModel : ViewModelBase
    {
        [IgnoreDataMember]
        public override string UrlPathSegment
        {
            get { return "Main Page"; }
        }
         

		public string MainText { get { return "Main Page"; } }
         

        [DataMember]
        public string SomeText
        {
            get;set;
        }
		public ReactiveCommand<Unit, ISampleRoutableViewModel> NavigateToSecondPage { get; private set;}


        public MainPageViewModel(ISampleScreen screen = null) : base(screen)
        {
            NavigateToSecondPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new SecondPageViewModel(screen)));
				                                                         
        }
    }

}
