using Microsoft.EntityFrameworkCore;

namespace WebOopPrac_Api.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UniqueID { get; set; }
    }

    public class UserCred
    {
		public int Id { get; set; }
		public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial {  get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool TermsCondition { get; set; }
    }
}
