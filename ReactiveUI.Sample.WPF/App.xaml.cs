using Akavache;
using Newtonsoft.Json;
using ReactiveUI.XamlForms.Sample;
using Splat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReactiveUI.Sample.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            BlobCache.ApplicationName = "ReactiveUI.WPF.Sample";
            Initialize();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            BlobCache.Shutdown().Wait();
        }

        AutoSuspendHelper autoSuspendHelper;
        public AutoSuspendHelper Initialize()
        {
            var scheduler = RxApp.MainThreadScheduler;

            //Make sure this is called after something accesses RxApp so that RxApp can register its defaults
            //And then this can override those
            Registrations.Register(Locator.CurrentMutable);

            autoSuspendHelper = new AutoSuspendHelper(this);
            RxApp.SuspensionHost.SetupDefaultSuspendResume();


            RxApp.SuspensionHost.CreateNewAppState = () =>
            {
                //Ensure App has initialize
                //Xam Forms gets annoyed if you don't have a page on the
                //Navigation stack so this just ensures that it is there
                var bootStrapper = new AppBootstrapper();
                bootStrapper.Init();
                return bootStrapper;
            };

            return autoSuspendHelper;
        }
    }


    //For some reason the fallback on the internals of wpf reactiveui isn't calling CreateNewAppState
    public class WPFAkavacheDriver : ISuspensionDriver, IEnableLogger
    {
        public IObservable<object> LoadState()
        {
            return BlobCache.UserAccount.GetObject<object>("__AppState")
                .LoggedCatch(this, Observable.Return(RxApp.SuspensionHost.CreateNewAppState()));
        }

        public IObservable<Unit> SaveState(object state)
        {
            return BlobCache.UserAccount.InsertObject("__AppState", state)
                .SelectMany(BlobCache.UserAccount.Flush());
        }

        public IObservable<Unit> InvalidateState()
        {
            return BlobCache.UserAccount.InvalidateObject<object>("__AppState");
        }
    }

    public class Registrations
    {
        public static void Register(IMutableDependencyResolver resolver)
        {
            resolver.Register(() => new JsonSerializerSettings()
            {
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.All,
            }, typeof(JsonSerializerSettings), null);

            var akavacheDriver = new WPFAkavacheDriver();
            resolver.Register(() => akavacheDriver, typeof(ISuspensionDriver), null);

            // NB: These correspond to the hacks in Akavache.Http's registrations
            resolver.Register(() => resolver.GetService<ISuspensionHost>().ShouldPersistState,
                typeof(IObservable<IDisposable>), "ShouldPersistState");

            resolver.Register(() => resolver.GetService<ISuspensionHost>().IsUnpausing,
                typeof(IObservable<Unit>), "IsUnpausing");

            resolver.Register(() => RxApp.TaskpoolScheduler, typeof(IScheduler), "Taskpool");
        }
    }
}
