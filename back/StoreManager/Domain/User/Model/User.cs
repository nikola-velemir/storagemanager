using StoreManager.Infrastructure.User.Model;

namespace StoreManager.Domain.User.Model
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public User(string username, string password, string firstName, string lastName, UserRole role)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }
        public User(int id, string username, string password, string firstName, string lastName, UserRole role)
        {
            Id = id;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
        }

        public override bool Equals(object? obj)
        {
            return obj is User model &&
                   Id == model.Id &&
                   Username == model.Username &&
                   Password == model.Password &&
                   FirstName == model.FirstName &&
                   LastName == model.LastName &&
                   Role == model.Role;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, Password, FirstName, LastName, Role);
        }

        public bool Equals(User? other)
        {
            return other != null &&
             Username == other.Username &&
             Password == other.Password &&
             FirstName == other.FirstName &&
             LastName == other.LastName &&
             Role == other.Role;
        }
    }
}
