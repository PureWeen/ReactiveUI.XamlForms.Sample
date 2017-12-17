using System;
using System.Collections.Generic;
using System.Text;

namespace ReactiveUI.XamlForms.Sample
{
    public interface ISampleRoutableViewModel
    { 
        string UrlPathSegment { get; }
        ISampleScreen HostScreen { get; }
    }
}
