using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using System.Diagnostics;
using Akavache.Duck;

namespace Akavache.Mobile
{
    public class AkavacheDriver : ISuspensionDriver, IEnableLogger
    {

        public IObservable<object> LoadState()
        {
            return Locator.Current.GetService<IBlobWrapper>().GetObject<object>("__AppState");
        }

        public IObservable<Unit> SaveState(object state)
        {
            return Locator.Current.GetService<IBlobWrapper>().InsertObject<object>("__AppState", state);
        }

        public IObservable<Unit> InvalidateState()
        {
            return Locator.Current.GetService<IBlobWrapper>().InvalidateObject<object>("__AppState");
        }
    }
}