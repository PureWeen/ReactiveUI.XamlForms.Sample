using Splat;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ReactiveUI.XamlForms.Sample.ViewModels
{
    [DataContract]
    public abstract class ViewModelBase
         : ReactiveObject, ISampleRoutableViewModel, ISupportsActivation
    {
        ISampleScreen _HostScreen;

        [IgnoreDataMember]
        public ViewModelActivator Activator
        {
            get;
            private set;
        }
        [IgnoreDataMember]
        public ISampleScreen HostScreen =>
            _HostScreen ?? Locator.Current.GetService<ISampleScreen>();

        public abstract string UrlPathSegment { get; }


        public ViewModelBase(ISampleScreen screen = null)
        {
            Activator = new ViewModelActivator();
            _HostScreen = screen;
        }
    }
}
