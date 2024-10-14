using ToDoApp.Data;
using ToDoApp.Data.Models;
using ToDoApp.Service;
using ToDoApp.ViewModels.Base;

namespace ToDoApp.ViewModels
{
    public class ToDoItemPageViewModel : BasePageViewModel
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; }

        public Command SaveCommand { get ; set; }

        public Command DeleteCommand { get; set; }


        public ToDoItemPageViewModel(IAppDatabase appDatabase, INavigationService navigationService) : base(appDatabase, navigationService)
        {
            SaveCommand = new Command(async () => { await SaveItem(); });
            DeleteCommand = new Command(async () => { await DeleteItem(); });
        }

        public async Task SaveItem()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Name Required", "Please enter a name for the todo item.", "OK");
                return;
            }

            await Database.SaveOrUpdateToDoItem(new ToDoItem()
            {
                Id = Id,
                ListId = ListId,
                Name = Name,  
                Notes = Notes,
                Done = Done,
            });

            await NavigationService.PopAsync();
        }

        public async Task DeleteItem()
        {
            if (Id == Guid.Empty)
                return;

            await Database.DeleteItemAsync(new ToDoItem()
            {
                Id = Id,
                ListId = ListId,
                Name = Name,
                Notes = Notes,
                Done = Done,
            });

            await NavigationService.PopAsync();
        }
    }
}
