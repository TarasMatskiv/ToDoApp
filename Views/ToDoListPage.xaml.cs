using ToDoApp.ViewModels;
using ToDoApp.ViewModels.Collections;
using ToDoApp.Views.Base;

namespace ToDoApp.Views;

public partial class ToDoListPage : ToDoListPageXaml
{
	public ToDoListPage()
	{
		InitializeComponent();
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ViewModel?.LoadData();
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ToDoItemViewModel item)
            return;

        await ViewModel.OpenToDoItemPage(item);
    }
}

public abstract class ToDoListPageXaml : BasePage<ToDoListPageViewModel>
{

}