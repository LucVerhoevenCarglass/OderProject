using System;
using System.Collections.Generic;
using Order.Domain.Users;

namespace Order.Databases
{
    public static class UsersDatabase
    {
        public static List<User> Users = new List<User>
        {
            new User() { Email = "admin@oder.com",  Password= "admin", UserRole = User.Roles.Admin},
            new User() { Email = "customer@oder.com",  Password= "test"}
        };

        public static void InitDatabase()
        {
            Users.Clear();
            Users.Add(new User() { Email = "admin@oder.com", Password = "admin", UserRole = User.Roles.Admin });
            Users.Add(new User() { Email = "customer@oder.com", Password = "test" });
        }
    }
}
