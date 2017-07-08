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

        object _state = null;

        public IObservable<object> LoadState()
        {
            return Observable.Return(_state);
        }

        public IObservable<Unit> SaveState(object state)
        {
            this._state = state;
            return Observable.Return(Unit.Default);
        }

        public IObservable<Unit> InvalidateState()
        {
            _state = null;
            return Observable.Return(Unit.Default);
        }

        //     public IObservable<object> LoadState()
        //     {
        //return BlobCache.UserAccount.GetObject<object>("__AppState");
        //     }

        //     public IObservable<Unit> SaveState(object state)
        //     {
        //         return BlobCache.UserAccount.InsertObject("__AppState", state)
        //             .SelectMany(BlobCache.UserAccount.Flush());
        //     }

        //     public IObservable<Unit> InvalidateState()
        //     {
        //         return BlobCache.UserAccount.InvalidateObject<object>("__AppState");
        //     }
    }
}