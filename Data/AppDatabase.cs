using SQLite;
using ToDoApp.Data.Models;

namespace ToDoApp.Data
{
    public class AppDatabase : IAppDatabase
    {
        SQLiteAsyncConnection Database;

        public AppDatabase()
        {
        }

        public async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<ToDoListItem>();
            await Database.CreateTableAsync<ToDoItem>();
        }

        public async Task<List<ToDoItem>> GetListToDos(Guid id)
        {
            await Init();
            return await Database.Table<ToDoItem>().Where(toDoItem => toDoItem.ListId == id).ToListAsync();
        }

        public async Task<List<ToDoListItem>> GetAllToDolLists()
        {
            await Init();
            return await Database.Table<ToDoListItem>().ToListAsync();
        }

        public async Task<ToDoItem> GetToDoItem(Guid id)
        {
            await Init();
            return await Database.Table<ToDoItem>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<ToDoListItem> GetToDoList(Guid id)
        {
            await Init();
            return await Database.Table<ToDoListItem>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> SaveOrUpdateToDoItem(ToDoItem item)
        {
            await Init();
            if (await Database.Table<ToDoItem>().FirstOrDefaultAsync(x => x.Id == item.Id) != null)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> SaveOrUpdateToDoList(ToDoListItem item)
        {
            await Init();
            if (await Database.Table<ToDoListItem>().FirstOrDefaultAsync(x => x.Id == item.Id) != null)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(ToDoItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }

        public async Task<int> DeleteListItemAsync(ToDoListItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
