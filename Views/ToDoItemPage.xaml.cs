using ToDoApp.ViewModels;
using ToDoApp.Views.Base;

namespace ToDoApp.Views;

public partial class ToDoItemPage : ToDoItemPageXaml
{
    public ToDoItemPage()
    {   
        InitializeComponent();
    }
}

public abstract class ToDoItemPageXaml : BasePage<ToDoItemPageViewModel>
{

}