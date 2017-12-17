using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace ReactiveUI.XamlForms.Sample
{
    public interface ISampleRoutingState
    {
        ISampleRoutableViewModel ModalNavigationViewModel { get; }
        ReactiveCommand<Unit, Unit> NavigateBack { get; }
        ReactiveCommand<ISampleRoutableViewModel, ISampleRoutableViewModel> Navigate { get; }
        ReactiveCommand<ISampleRoutableViewModel[], ISampleRoutableViewModel> NavigateAndReset { get; }

        ReactiveCommand<ISampleRoutableViewModel, ISampleRoutableViewModel> NavigateModal { get; }

        IReadOnlyList<ISampleRoutableViewModel> NavigationStack { get; }
        IObservable<ISampleRoutableViewModel> Load(Func<ISampleRoutableViewModel> initialViewModel);
    }
}
