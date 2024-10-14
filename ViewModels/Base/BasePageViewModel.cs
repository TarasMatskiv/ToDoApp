using ReactiveUI;
using ToDoApp.Data;
using ToDoApp.MVVM;
using ToDoApp.Service;

namespace ToDoApp.ViewModels.Base
{
    public class BasePageViewModel : ReactiveObject, IViewModel
    {
        public IAppDatabase Database { get; set; }

        public INavigationService NavigationService { get; set; }

        public BasePageViewModel(IAppDatabase appDatabase, INavigationService navigationService)
        {
            Database = appDatabase;
            NavigationService = navigationService;
        }

        public void SetState<T>(Action<T> action) where T : class, IViewModel
        {
            action(this as T);
        }
    }
}
