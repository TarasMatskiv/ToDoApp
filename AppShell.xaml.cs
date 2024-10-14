using ToDoApp.MVVM;
using ToDoApp.ViewModels;
using ToDoApp.Views;

namespace ToDoApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            PageHome.BindingContext = Resolver.Resolve<HomePageViewModel>();

            Routing.RegisterRoute(nameof(ToDoItemPage), typeof(ToDoItemPage));
            Routing.RegisterRoute(nameof(ToDoListPage), typeof(ToDoListPage));
        }
    }
}
