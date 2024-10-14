using Autofac;
using Autofac.Features.OwnedInstances;
using ReactiveUI;

namespace ToDoApp.MVVM
{
    public class ViewFactory : IViewFactory
    {
        private readonly IComponentContext _componentContext;
        private readonly Dictionary<Type, Type> _map = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _reversedMap = new Dictionary<Type, Type>();

        public ViewFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
            _registry = new Dictionary<IViewModel, Action>();
        }

        public void Register<TViewModel, TView>()
            where TViewModel : class, IViewModel
            where TView : IPage
        {
            var vm = typeof(TViewModel);
            var v = typeof(TView);
            _map[vm] = v;
            _reversedMap[v] = vm;

#if DEBUG
            //_logger.Debug($"Registered view: {v} - { vm}");
#endif
        }

        public void Register(Type viewModel, Type view)
        {
            if (!viewModel.IsAssignableTo<IViewModel>())
                throw new ArgumentException("viewModel must be type of IViewModel");

            if (!view.IsAssignableTo<IPage>())
                throw new ArgumentException("view must be type of Page");

            _map[viewModel] = view;
            _reversedMap[view] = viewModel;

#if DEBUG
            //_logger.Debug($"Registered view: {view} - { viewModel}");
#endif
        }

        public Type ResolveViewType(Type modelType)
        {
            Type viewType;
            _map.TryGetValue(modelType, out viewType);

            return viewType;
        }

        public IPage Resolve<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : class, IViewModel
        {
            TViewModel viewModel;
            return Resolve(out viewModel, setStateAction);
        }

        public IViewModel ResolveReversed<TView>() where TView : IPage
        {
            TView view;
            return ResolveReversed(out view);
        }

        public IPage Resolve<TViewModel>(out TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            var ownedVM = _componentContext.Resolve<Owned<TViewModel>>();
            viewModel = ownedVM.Value;
            if (!_registry.ContainsKey(viewModel))
                _registry.Add(viewModel, ownedVM.Dispose);

            Type viewType;
            _map.TryGetValue(typeof(TViewModel), out viewType);

            if (viewType == null)
                throw new Exception($"View not registered for type: {typeof(TViewModel).Name}");

            var view = _componentContext.Resolve(viewType) as IPage;

            if (setStateAction != null)
                viewModel.SetState(setStateAction);
                
            SetBindingContextAndNavigation(view, viewModel);
            return view;
        }

        public IViewModel ResolveViewModel(Type viewModelType,
            Action<IViewModel> setStateAction = null)
        {
            var viewModel = _componentContext.Resolve(viewModelType) as IViewModel;

            if (setStateAction != null)
                viewModel.SetState(setStateAction);

            return viewModel;
        }

        public IViewModel ResolveViewModel<TViewModel>(Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            var viewModel = _componentContext.Resolve<TViewModel>() as IViewModel;

            if (setStateAction != null)
                viewModel.SetState(setStateAction);

            return viewModel;
        }

        public IViewModel ResolveReversed<TView>(out TView view)
            where TView : IPage
        {
            var ownedView = _componentContext.Resolve<Owned<TView>>();
            view = ownedView.Value;

            Type vmType;
            _reversedMap.TryGetValue(typeof(TView), out vmType);

            if (vmType == null)
                throw new Exception($"ViewModel not registered for type: {typeof(TView).Name}");

            var viewModel = _componentContext.Resolve(vmType) as IViewModel;

            SetBindingContextAndNavigation(view, viewModel);
            return viewModel;
        }

        public IPage Resolve<TViewModel>(TViewModel viewModel, Action<TViewModel> setStateAction = null)
            where TViewModel : class, IViewModel
        {
            Type viewType;
            _map.TryGetValue(viewModel.GetType(), out viewType);

            if (viewType == null)
                throw new Exception($"View not registered for type: {viewModel.GetType()}");

            var view = _componentContext.Resolve(viewType) as IPage;
            SetBindingContextAndNavigation(view, viewModel);
            return view;
        }

        public IPage Resolve(Type viewModelType, Action<IViewModel> setStateAction = null)
        {
            if (!viewModelType.IsAssignableTo<IViewModel>())
                throw new ArgumentException("viewModelType must be type of IViewModel");

            Type viewType;
            _map.TryGetValue(viewModelType, out viewType);

            if (viewType == null)
                throw new Exception($"View not registered for type: {viewModelType.Name}");

            var view = _componentContext.Resolve(viewType) as IPage;
            var d = 0;
            var model = _componentContext.Resolve(viewModelType) as IViewModel;

            if (setStateAction != null)
                model.SetState(setStateAction);

            SetBindingContextAndNavigation(view, model);
            return view;
        }

        public IViewModel ResolveReversed<TView>(TView view)
            where TView : IPage
        {
            Type vmType;
            _reversedMap.TryGetValue(view.GetType(), out vmType);

            if (vmType == null)
                throw new Exception($"ViewModel not registered for type: {view.GetType().Name}");

            var viewModel = _componentContext.Resolve(vmType) as IViewModel;
            SetBindingContextAndNavigation(view, viewModel);
            return viewModel;
        }

        private void SetBindingContextAndNavigation<TView, TViewModel>(TView view, TViewModel viewModel)
            where TView : IPage
            where TViewModel : class, IViewModel
        {
            if (view is IViewFor viewFor)
            {
                viewFor.ViewModel = viewModel;
            }
            else
            {
                throw new Exception("No View For");
            }
        }

        private readonly Dictionary<IViewModel, Action> _registry;
    }
}
