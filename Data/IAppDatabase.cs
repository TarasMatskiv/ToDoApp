using ToDoApp.Data.Models;

namespace ToDoApp.Data
{
    public interface IAppDatabase
    {
        Task Init();

        Task<List<ToDoItem>> GetListToDos(Guid id);

        Task<List<ToDoListItem>> GetAllToDolLists();

        Task<ToDoItem> GetToDoItem(Guid id);

        Task<ToDoListItem> GetToDoList(Guid id);

        Task<int> SaveOrUpdateToDoItem(ToDoItem item);

        Task<int> SaveOrUpdateToDoList(ToDoListItem item);

        Task<int> DeleteItemAsync(ToDoItem item);

        Task<int> DeleteListItemAsync(ToDoListItem item);
    }
}
