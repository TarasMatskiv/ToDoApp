namespace ToDoApp.MVVM
{
    public interface IViewFactory
    {
        void Register<TViewModel, TView>()
        where TViewModel : class, IViewModel
        where TView : IPage;

        void Register(Type viewModel, Type view);

        Type ResolveViewType(Type modelType);
        IViewModel ResolveViewModel(Type viewModelType, Action<IViewModel> setStateAction = null);
        IViewModel ResolveViewModel<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : class, IViewModel;

        IPage Resolve(Type viewModelType, Action<IViewModel> setStateAction = null);

        IPage Resolve<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;

        IViewModel ResolveReversed<TView>() where TView : IPage;

        IPage Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;

        IViewModel ResolveReversed<TView>(out TView view)
            where TView : IPage;

        IPage Resolve<TViewModel>(TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel;

        IViewModel ResolveReversed<TView>(TView view)
            where TView : IPage;
    }
}
