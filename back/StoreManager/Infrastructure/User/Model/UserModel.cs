using System.ComponentModel.DataAnnotations;

namespace StoreManager.Infrastructure.User.Model
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public UserModel(string username, string password, string firstName, string lastName, UserRole role)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
        public UserModel(int id, string username, string password, string firstName, string lastName, UserRole role)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
    }
}
