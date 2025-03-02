namespace StoreManager.Infrastructure.User.Model
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserModel(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
