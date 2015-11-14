namespace PI_lab2.Models.DataModels
{
    public class User
    {
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
