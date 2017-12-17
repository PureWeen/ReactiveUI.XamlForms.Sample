using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using System.Diagnostics;

namespace Akavache.Mobile
{
    public class AkavacheDriver : ISuspensionDriver, IEnableLogger
    {
        object _state;
        public IObservable<object> LoadState()
        {
            // this mainly happens with android where activity is destroyed
            // but everything else stays in memory
            if (_state != null)
            {
                return Observable.Return(_state);
            }

            return BlobCache.UserAccount.GetObject<object>("__AppState");
        }

        public IObservable<Unit> SaveState(object state)
        {
            return BlobCache.UserAccount.InsertObject("__AppState", state)
                .SelectMany(BlobCache.UserAccount.Flush())
                .Do(_=> _state = state);
        }

        public IObservable<Unit> InvalidateState()
        {
            return BlobCache.UserAccount.InvalidateObject<object>("__AppState");
        }
    }
}