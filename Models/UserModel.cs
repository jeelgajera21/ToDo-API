namespace ToDo_API.Models
{
    public class UserModel
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
