 using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Collections;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Reactive.Concurrency;
using System.Reactive.Threading.Tasks;
using System.Runtime.Serialization;
using Splat;

namespace ReactiveUI.XamlForms.Sample
{
    /// <summary>
    /// RoutingState manages the ViewModel Stack and allows ViewModels to
    /// navigate to other ViewModels.
    /// </summary>
    [DataContract]
    public class SampleRoutingState : ReactiveObject, ISampleRoutingState, IEnableLogger
    { 
          
        [IgnoreDataMember]
        public IReadOnlyList<ISampleRoutableViewModel> NavigationStack =>
            NavigationForm
                .Navigation
                .NavigationStack
                .Select(x => (ISampleRoutableViewModel)x.BindingContext)
                .ToImmutableList();
        
        
        [IgnoreDataMember]
        public ISampleRoutableViewModel ModalNavigationViewModel =>
            NavigationForm
                .Navigation
                .ModalStack
                .Select(x => (ISampleRoutableViewModel)x.BindingContext)
                .FirstOrDefault();
        

        [IgnoreDataMember] private IScheduler _scheduler;

        /// <summary>
        /// The scheduler used for commands. Defaults to <c>RxApp.MainThreadScheduler</c>.
        /// </summary>
        [IgnoreDataMember]
        public IScheduler Scheduler {
            get => this._scheduler;
            set {
                if (this._scheduler == value) return;
                
                this._scheduler = value;
                this.setupRx();
            }
        }

        /// <summary>
        /// Navigates back to the previous element in the stack.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<Unit, Unit> NavigateBack { get; protected set; }

        /// <summary>
        /// Navigates to the a new element in the stack - the Execute Sampleameter
        /// must be a ViewModel that implements ISampleRoutableViewModel.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<ISampleRoutableViewModel, ISampleRoutableViewModel> Navigate { get; protected set; }

        /// <summary>
        /// Navigates to a new element and resets the navigation stack (i.e. the
        /// new ViewModel will now be the only element in the stack) - the
        /// Execute Sampleameter must be a ViewModel that implements
        /// ISampleRoutableViewModel.
        /// </summary>
        [IgnoreDataMember]
        public ReactiveCommand<ISampleRoutableViewModel[], ISampleRoutableViewModel> NavigateAndReset { get; protected set; }

        [IgnoreDataMember]
        public ReactiveCommand<ISampleRoutableViewModel, ISampleRoutableViewModel> NavigateModal { get; protected set; }
         
 
        [OnDeserialized]
        void setupRx(StreamingContext sc) { setupRx();  }


        private SampleRoutedViewHost NavigationForm =>
            SampleRoutedViewHost.Instance;
        
        void setupRx()
        {
            _scheduler = this._scheduler ?? RxApp.MainThreadScheduler;
            NavigateBack = 
                ReactiveCommand.CreateFromTask(async () =>
                {
                    if (ModalNavigationViewModel != null)
                        await NavigationForm.Navigation.PopModalAsync();
                    
                    await NavigationForm.PopAsync();
                },
                outputScheduler: this.Scheduler);

            Navigate = ReactiveCommand.CreateFromTask<ISampleRoutableViewModel, ISampleRoutableViewModel>(async x => {
              
                var page = NavigationForm.PageForViewModel(x);
                await NavigationForm.PushAsync(page);
                return x;
                
            },
            outputScheduler: this.Scheduler);

            NavigateAndReset = ReactiveCommand.CreateFromTask<ISampleRoutableViewModel[], ISampleRoutableViewModel>(
                async newStack => {
               
                    var pages = 
                        newStack
                            .Select(x=> NavigationForm.PageForViewModel(x))
                            .ToArray();

                    
                    if (ModalNavigationViewModel != null)
                    {
                        await NavigationForm.Navigation.PopModalAsync();
                    }

                    NavigationForm.Navigation.InsertPageBefore(pages.Last(),
                        NavigationForm.Navigation.NavigationStack[0]);

                    await NavigationForm.Navigation.PopToRootAsync();

                    for (int i = 0; i < pages.Length - 1; i++)
                    {
                        NavigationForm.Navigation.InsertPageBefore(pages[i], pages.Last());
                    }
                    
                    return newStack.Last();

            },
            outputScheduler: this.Scheduler);
            
            
            NavigateModal = ReactiveCommand.CreateFromTask<ISampleRoutableViewModel, ISampleRoutableViewModel>(
                async x => {

                    if (x == null)
                    {
                        if (ModalNavigationViewModel == null)
                            return null;

                        var modalPage = await NavigationForm.Navigation
                            .PopModalAsync();

                        return (ISampleRoutableViewModel) modalPage.BindingContext;
                    }
                    
                    var page = NavigationForm.PageForViewModel(x);
                    
                    await NavigationForm.Navigation.PushModalAsync(page);
                        
                    return x;
                },
                outputScheduler: this.Scheduler);
             
            

            NavigateBack
                .ThrownExceptions
                .Subscribe((Exception exc) =>
                {
                    this.Log().ErrorException("NavigateBack", exc);
                });

            Navigate
                .ThrownExceptions
                .Subscribe((Exception exc) =>
                {
                    this.Log().ErrorException("Navigate", exc);
                });

            NavigateAndReset
                .ThrownExceptions
                .Subscribe((Exception exc) =>
                {
                    this.Log().ErrorException("NavigateAndReset", exc);
                });

            NavigateModal
                .ThrownExceptions
                .Subscribe((Exception exc) =>
                {
                    this.Log().ErrorException("NavigateModal", exc);
                });



            Navigate.Select(_ => Unit.Default)
                .Merge(NavigateAndReset.Select(_ => Unit.Default))
                .Merge(NavigateModal.Select(_ => Unit.Default))
                .Merge(NavigateBack.Select(_ => Unit.Default))
                .Subscribe(_ =>
                {
                    _NavigationStack = NavigationStack.ToArray();
                });
        }

        [IgnoreDataMember]
        static SampleRoutingState _instance;
        [DataMember] ISampleRoutableViewModel[] _NavigationStack;


        public IObservable<ISampleRoutableViewModel> Load(Func<ISampleRoutableViewModel> initialViewModel)
        {
            if (_NavigationStack == null)
                return NavigateAndReset.Execute(new[] { initialViewModel() });

            return NavigateAndReset.Execute(_NavigationStack.ToArray());
        }
        public SampleRoutingState()
		{ 
		    setupRx();
			if (_instance != null)
				throw new Exception("Main Page View Model already instantiated");

			_instance = this;
        }
    }
}
