using System.Collections.ObjectModel;
using ToDoApp.Data;
using ToDoApp.Data.Models;
using ToDoApp.Service;
using ToDoApp.ViewModels.Base;
using ToDoApp.ViewModels.Collections;

namespace ToDoApp.ViewModels
{
    public class ToDoListPageViewModel : BasePageViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ObservableCollection<ToDoItemViewModel> ToDos { get; set; } = new ();

        public Command SaveCommand { get; set; }

        public Command AddToDoItemCommand { get; set; }

        public Command OpenToDoItemPageCommange { get; set; }

        public Command DeleteCommand { get; set; }

        public ToDoListPageViewModel(IAppDatabase appDatabase, INavigationService navigationService) : base(appDatabase, navigationService)
        {
            SaveCommand = new Command(async () => { await SaveList(); });
            AddToDoItemCommand = new Command(async () => { await AddToDoItem(); });
            OpenToDoItemPageCommange = new Command(async () => { await OpenToDoItemPage(); });
            DeleteCommand = new Command(async () => { await DeleteList(); });

        }

        public async Task LoadData()
        {
            var items = await Database.GetListToDos(Id);
            ToDos.Clear();
            items.ForEach(item => ToDos.Add(new ToDoItemViewModel(item)));
        }

        public async Task SaveList()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Name Required", "Please enter a name for the list item.", "OK");
                return;
            }
            var item = new ToDoListItem()
            {
                Id = Id,
                Name = Name,
            };
            await Database.SaveOrUpdateToDoList(item);
            await NavigationService.PopAsync();
        }

        public async Task AddToDoItem()
        {
            var itemId = Guid.NewGuid();
            await NavigationService.PushAsync<ToDoItemPageViewModel>(vm =>
            {
                vm.Id = itemId;
                vm.ListId = Id;
            });
        }

        public async Task OpenToDoItemPage(ToDoItemViewModel item = null)
        {
            await NavigationService.PushAsync<ToDoItemPageViewModel>(vm =>
            {
                vm.Id = item.Id;
                vm.ListId = item.ListId;
                vm.Name = item.Name;
                vm.Notes = item.Notes;
                vm.Done = item.Done;
            });
        }

        public async Task DeleteList()
        {
            if (Id == Guid.Empty)
                return;
            var item = new ToDoListItem()
            {
                Id = Id,
                Name = Name,
            };
            await Database.DeleteListItemAsync(item);
            await NavigationService.PopAsync();
        }
    }
}
