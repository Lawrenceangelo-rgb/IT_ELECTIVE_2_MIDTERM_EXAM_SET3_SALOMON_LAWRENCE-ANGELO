using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

public interface IUserRepository
{
    bool ValidateCredentials(string username, string password);
    void AddUser(User user);
}

public class UserRepository : IUserRepository
{
    private static List<User> _users = new List<User>
    {
        new User { Id = 1, FirstName = "Admin", LastName = "Guard", Email = "admin@abc.com", Username = "admin", Password = "password123" }
    };

    public bool ValidateCredentials(string username, string password)
    {
        return _users.Any(u => u.Username == username && u.Password == password);
    }

    public void AddUser(User user)
    {
        user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(user);
    }
}