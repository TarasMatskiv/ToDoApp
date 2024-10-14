using ToDoApp.ViewModels;
using ToDoApp.ViewModels.Collections;
using ToDoApp.Views.Base;

namespace ToDoApp.Views;

public partial class HomePage : HomePageXaml
{
    public HomePage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel?.GetData();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ToDoListItemViewModel item)
            return;

        ViewModel?.OpenToDoListPage(item);
    }
}

public abstract class HomePageXaml : BasePage<HomePageViewModel>
{

}