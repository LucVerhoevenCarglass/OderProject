using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Databases;
using Order.Domain.Users;
using Microsoft.Extensions.Logging;
using Order.Domain;

namespace Order.Services.Users
{
    public class UserService: IUserService
    {
        private const string ErrorMessage = "UserService : ";
        private readonly ILogger<UserService> _logger;
        private readonly IUsersDatabase _usersDatabase;


        public UserService(ILogger<UserService> logger, IUsersDatabase usersDatabase)
        {
            _logger = logger;
            _usersDatabase = usersDatabase;
        }

        public User CreateNewUser(User userToCreate)
        {
            _usersDatabase.AddUserIfNotExist(userToCreate);
            //if (_usersDatabase.Users.Any(user => user.Email.ToLower() == userToCreate.Email.ToLower()))
            //{
            //    _logger.LogError($"{ErrorMessage} Mail already exists: {userToCreate.Email}");
            //    throw new OrderExeptions($"{ErrorMessage} Mail already exists");
            //}
            //_usersDatabase.Users.Add(userToCreate);
            return userToCreate;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await Task.Run(() => _usersDatabase.CheckUserInDatabase(email, password));
            //var user = await Task.Run(() => _usersDatabase.Users.SingleOrDefault(login => login.Email == email && login.Password == password));
            //if (user == null)
            //{
            //    _logger.LogError($"{ErrorMessage} Mail not yet registered: {email}");
            //    return null;
            //}
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _usersDatabase.GetDatabase();
        }
    }
}
