using Microsoft.EntityFrameworkCore;

namespace WebOopPrac_Api.Models
{
    public class TodoModel
    {
        public class TodoItem
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public bool IsCompleted { get; set; }
        }
    }
}
