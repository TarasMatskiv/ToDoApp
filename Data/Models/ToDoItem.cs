using SQLite;

namespace ToDoApp.Data.Models
{
    public class ToDoItem
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        public Guid ListId { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; }
    }
}
