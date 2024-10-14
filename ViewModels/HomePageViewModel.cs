using System.Collections.ObjectModel;
using ToDoApp.Data;
using ToDoApp.Service;
using ToDoApp.ViewModels.Base;
using ToDoApp.ViewModels.Collections;

namespace ToDoApp.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        public ObservableCollection<ToDoListItemViewModel> List { get; set; } = new();

        public Command AddCommand { get; set; }

        public HomePageViewModel(IAppDatabase appDatabase, INavigationService navigationService) : base(appDatabase, navigationService)
        {
            AddCommand = new Command(async () =>
            {
                await NavigationService.PushAsync<ToDoListPageViewModel>(vm =>
                {
                    vm.Id = Guid.NewGuid();
                });
            });
        }

        public async Task GetData()
        {
            var items = await Database.GetAllToDolLists();
            List.Clear();
            items.ForEach(item => List.Add(new ToDoListItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
            }));
        }

        public async Task OpenToDoListPage(ToDoListItemViewModel item)
        {
            await NavigationService.PushAsync<ToDoListPageViewModel>(vm =>
            {
                vm.Id = item.Id;
                vm.Name = item.Name;
            });
        }
    }
}
