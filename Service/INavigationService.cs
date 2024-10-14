using ToDoApp.MVVM;

namespace ToDoApp.Service
{
    public interface INavigationService
    {
        Task PushAsync<T>(Action<T> setStateAction = null) where T : class, IViewModel;

        Task PopAsync();
    }
}
