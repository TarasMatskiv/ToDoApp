namespace ToDoApp.MVVM
{
    public interface IViewModel
    {
        void SetState<T>(Action<T> action) where T : class, IViewModel;
    }
}
