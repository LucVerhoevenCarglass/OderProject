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


        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public User CreateNewUser(User userToCreate)
        {
            if (UsersDatabase.Users.Any(user => user.Email.ToLower() == userToCreate.Email.ToLower()))
            {
                _logger.LogError($"{ErrorMessage} Mail already exists: {userToCreate.Email}");
                throw new OrderExeptions($"{ErrorMessage} Mail already exists");
            }
            UsersDatabase.Users.Add(userToCreate);
            return userToCreate;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await Task.Run(() => UsersDatabase.Users.SingleOrDefault(login => login.Email == email && login.Password == password));
            if (user == null)
            {
                _logger.LogError($"{ErrorMessage} Mail not yet registered: {email}");
                return null;
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UsersDatabase.Users;
        }
    }
}
