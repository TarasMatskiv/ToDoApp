using ReactiveUI.Maui;
using ToDoApp.MVVM;
using ToDoApp.ViewModels.Base;

namespace ToDoApp.Views.Base
{
    public class BasePage<TViewModel> : ReactiveContentPage<TViewModel>, IPage where TViewModel : BasePageViewModel
    {

    }
}
