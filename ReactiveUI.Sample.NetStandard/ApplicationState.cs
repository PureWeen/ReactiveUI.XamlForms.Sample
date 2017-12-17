using System;
using ReactiveUI;
using Splat;

#if !NET_45
using Xamarin.Forms;
using ReactiveUI.XamForms;
#endif

using Akavache;
using System.Runtime.Serialization;
using System.Linq;
using ReactiveUI.XamlForms.Sample.ViewModels;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveUI.XamlForms.Sample
{
	[DataContract]
    public class ApplicationState : ReactiveObject, ISampleScreen
    {

        [DataMember]
        public SampleRoutingState Router
        {
            get;
            set;
        }


        public ApplicationState()
        {
        }

        //The init is only called from CreateNewAppState... If this comes from the CACHE
        //then the Router is already instantiated and set to the correct VM
        //I await the navigation because Xam Forms can get annoyed if there's not a 
        //Page on the stack once it starts up
        public void Init()
        {
            Router = new SampleRoutingState();
        }
    }
}
