using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using Xamarin.Forms;

namespace ReactiveUI.XamlForms.Sample
{
    public class SampleRoutedViewHost : NavigationPage, IActivatable, IEnableLogger
    {
        public static readonly BindableProperty RouterProperty = BindableProperty.Create(
           nameof(Router),
           typeof(ISampleRoutingState),
           typeof(SampleRoutedViewHost),
           default(ISampleRoutingState),
           BindingMode.OneWay);

        public ISampleRoutingState Router
        {
            get => (ISampleRoutingState)GetValue(RouterProperty);
            set => SetValue(RouterProperty, value);
        }
        public static SampleRoutedViewHost Instance { get; internal set; }

        public SampleRoutedViewHost(Page startingPage) : base(startingPage)
        {
            Instance = this;
        }

        public Page PageForViewModel(ISampleRoutableViewModel vm)
        {
            vm = vm ?? throw new ArgumentException("vm cannot be null");

            var ret = ViewLocator.Current.ResolveView(vm);
            if (ret == null)
            {
                var msg = String.Format(
                    "Couldn't find a View for ViewModel. You probably need to register an IViewFor<{0}>",
                    vm.GetType().Name);

                throw new Exception(msg);
            }

            ret.ViewModel = vm;

            var pg = (Page)ret;
            pg.Title = vm.UrlPathSegment;

            return pg;
        }
    }
}
