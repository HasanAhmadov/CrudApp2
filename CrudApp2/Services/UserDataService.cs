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
                new User { Id = _userIdCounter++, Name = "Yunis Kazimli" },
                new User { Id = _userIdCounter++, Name = "Farhad Ragimov" },
                new User { Id = _userIdCounter++, Name = "Rahman Aslanov" },
                new User { Id = _userIdCounter++, Name  ="Hidayat Huseynov"}
            });
        }

        public static List<User> GetAllUsers()
        {
            return _users.ToList();
        }

        public static User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            user.Id = _userIdCounter++;
            _users.Add(user);
        }

        public bool UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                return true;
            }
            return false;
        }

        public bool DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
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
