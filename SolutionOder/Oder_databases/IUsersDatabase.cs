using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Users;

namespace Order.Databases
{
    public interface IUsersDatabase
    {
        void AddUserIfNotExist(User userToCreate);
        User CheckUserInDatabase(string email, string password);
        List<User> GetDatabase();
        void InitDatabase();
        void ClearDatabase();
    }
}
