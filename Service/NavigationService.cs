using ToDoApp.MVVM;

namespace ToDoApp.Service
{
    public class NavigationService : INavigationService
    {
        private IViewFactory viewFactory { get; set; }

        public NavigationService(IViewFactory factory) 
        {
            viewFactory = factory;
        }

        public async Task PopAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public async Task PushAsync<TViewModel>(Action<TViewModel> setStateAction = null) where TViewModel : class, IViewModel
        {
            var page = viewFactory.Resolve(setStateAction) as ContentPage;
            var viewModel = viewFactory.ResolveViewModel(setStateAction);

            page.BindingContext = viewModel;
            await Shell.Current.Navigation.PushAsync(page);
        }
    }
}
