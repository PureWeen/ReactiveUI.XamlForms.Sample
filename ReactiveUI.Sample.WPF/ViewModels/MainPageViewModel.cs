using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using Splat;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

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


		public ReactiveCommand<Unit, Unit> NavigateToSecondPage { get; private set;}


        public MainPageViewModel(IScreen screen = null)
        {
            Activator = new ViewModelActivator();

            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            NavigateToSecondPage = ReactiveCommand.Create(
                () =>
                {
                    StripedImage = new int[5];
                });


            this.WhenAnyValue(x => x.StripedImage)
                .ObserveOn(RxApp.TaskpoolScheduler)
                .Select(im => CreateBlurImage(im))
                .ToProperty(this, 
                    x => x.Filtered, 
                    out _filtered, scheduler: RxApp.MainThreadScheduler);

        }

        int[] CreateBlurImage(int[] im, double blur = 1.5)
        {
            if (im == null)
                return null;

            return im;
        }

        ObservableAsPropertyHelper<int[]> _filtered;
        public int[] Filtered => _filtered.Value;

        int[] _StripedImage;
        public int[] StripedImage
        {
            get
            {
                return _StripedImage;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _StripedImage, value);
            }
        }
    }

}
