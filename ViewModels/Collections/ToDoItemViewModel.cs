using ToDoApp.Data.Models;

namespace ToDoApp.ViewModels.Collections
{
    public class ToDoItemViewModel
    {
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; }

        public ToDoItemViewModel(ToDoItem item)
        {
            Id = item.Id;
            ListId = item.ListId;
            Name = item.Name;
            Notes = item.Notes;
            Done = item.Done;
        }
    }
}
