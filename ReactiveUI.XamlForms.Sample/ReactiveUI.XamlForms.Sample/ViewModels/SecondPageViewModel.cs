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


		public ViewModelActivator Activator
		{
			get;
			private set;
		}

		public string MainText { get { return "Second Page"; } }


        public SecondPageViewModel(IScreen screen = null)
		{
			Activator = new ViewModelActivator();

			HostScreen = screen ?? Locator.Current.GetService<IScreen>();
			NavigateBack = ReactiveCommand.CreateFromObservable(
				() => HostScreen.Router.NavigateBack.Execute(Unit.Default));
		}

    }
}
