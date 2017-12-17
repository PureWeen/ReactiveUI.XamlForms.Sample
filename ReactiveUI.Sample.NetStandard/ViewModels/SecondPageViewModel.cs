
using System.Reactive;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace ReactiveUI.XamlForms.Sample.ViewModels
{
   
    [DataContract]
    public class SecondPageViewModel : ViewModelBase
    {
		[IgnoreDataMember]
		public override string UrlPathSegment
		{
			get { return "second Page"; }
		}


		public ReactiveCommand<Unit, Unit> NavigateBack { get; private set; }
        public ReactiveList<GridViewItem> GridViewItems { get; private set; } = new ReactiveList<GridViewItem>();

	
		public string MainText { get { return "Second Page"; } }


        public SecondPageViewModel(ISampleScreen screen) : base(screen)
		{
            for(int i = 0; i < 500; i++)
            {
                GridViewItems.Add(new GridViewItem(i));
            }

			NavigateBack = ReactiveCommand.CreateFromObservable(
				() => HostScreen.Router.NavigateBack.Execute(Unit.Default));
		}



    }
}
