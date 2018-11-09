using System;
using System.Collections.Generic;
using System.Linq;
using Order.Domain;
using Order.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Order.Databases
{
    public class UsersDatabase:IUsersDatabase
    {
        private readonly List<User> Users;
        private const string ErrorMessage = "UserDatabase : ";
        private readonly ILogger<UsersDatabase> _logger;
        public UsersDatabase(ILogger<UsersDatabase> logger)
        {
            _logger = logger;
            Users = new List<User>();
            InitDatabase();
            //Users = new List<User>
            //{
            //    new User("IdAdmin") {Email = "admin@oder.com", Password = "admin", UserRole = User.Roles.Admin},
            //    new User("IdCustomer") {Email = "customer@oder.com", Password = "test"}
            //};
        }

        public List<User> GetDatabase()
        {
            return Users;
        }

        public void InitDatabase()
        {
            Users.Clear();
            Users.Add(new User("IdAdmin") { Email = "admin@oder.com", Password = "admin", UserRole = User.Roles.Admin });
            Users.Add(new User("IdCustomer") { Email = "customer@oder.com", Password = "test" });
        }

        public void ClearDatabase()
        {
            Users.Clear();
        }

        public void AddUserIfNotExist(User userToCreate)
        {
            if (Users.Any(user => user.Email.ToLower() == userToCreate.Email.ToLower()))
            {
                _logger.LogError($"{ErrorMessage} Mail already exists: {userToCreate.Email}");
                throw new OrderExeptions($"{ErrorMessage} Mail already exists");
            }
            Users.Add(userToCreate);
        }

        public User CheckUserInDatabase(string email, string password)
        {
            var user = Users.SingleOrDefault(login => login.Email == email && login.Password == password);
            if (user == null)
            {
                _logger.LogError($"{ErrorMessage} Mail not yet registered: {email}");
                throw new OrderExeptions($"{ErrorMessage} Mail not yet registered: {email}");
            }
            return user;
        }
    }
}
