
using System.Reactive;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.XamlForms.Sample.ViewModels
{
   
    [DataContract]
    public class SecondPageViewModel : ReactiveObject, IRoutableViewModel, ISupportsActivation
	{
		[IgnoreDataMember]
		public string UrlPathSegment
		{
			get { return "second Page"; }
		}

		[IgnoreDataMember]
		public IScreen HostScreen { get; protected set; }

		public ReactiveCommand<Unit, Unit> NavigateBack { get; private set; }
        public ReactiveList<GridViewItem> GridViewItems { get; private set; } = new ReactiveList<GridViewItem>();

		public ViewModelActivator Activator
		{
			get;
			private set;
		}

		public string MainText { get { return "Second Page"; } }


        public SecondPageViewModel(IScreen screen = null)
		{
            for(int i = 0; i < 500; i++)
            {
                GridViewItems.Add(new GridViewItem(i));
            }

			Activator = new ViewModelActivator();

			HostScreen = screen ?? Locator.Current.GetService<IScreen>();
			NavigateBack = ReactiveCommand.CreateFromObservable(
				() => HostScreen.Router.NavigateBack.Execute(Unit.Default));
		}



    }
}
