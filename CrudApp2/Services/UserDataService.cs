using CrudApp2.Models;

namespace CrudApp2.Services
{
    public class UserDataService
    {
        private static readonly List<User> _users = new List<User>();
        private static int _userIdCounter = 1;

        static UserDataService()
        {
            _users.AddRange(new[]
            {
                new User { UserId = _userIdCounter++, Name = "Yunis Kazimli" },
                new User { UserId = _userIdCounter++, Name = "Farhad Ragimov" },
                new User { UserId = _userIdCounter++, Name = "Rahman Aslanov" },
                new User { UserId = _userIdCounter++, Name  ="Hidayat Huseynov"}
            });
        }

        public static List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public static User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.UserId == id);
        }

        public void AddUser(User user)
        {
            user.UserId = _userIdCounter++;
            _users.Add(user);
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _users.Remove(user);
                OrderDataService._orders.RemoveAll(o => o.CreatedUserId == id);
                return true;
            }
            return false;
        }

    }
}
