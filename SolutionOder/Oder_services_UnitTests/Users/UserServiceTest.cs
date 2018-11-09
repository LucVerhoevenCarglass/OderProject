using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Order.Databases;
using Xunit;

namespace Order.Services.UnitTests.Users
{
    public class UserServiceTest
    {
        private readonly IUsersDatabase _usersDatabase;
        private readonly ICustomersDatabase _customersDatabase;
        public UserServiceTest(IUsersDatabase usersDatabase, ICustomersDatabase customersDatabase)
        {
            _usersDatabase = usersDatabase;
            _customersDatabase = customersDatabase;
            _usersDatabase.InitDatabase();
            _customersDatabase.ClearDatabase();
        }

        [Fact]
        public void CreateNewUser_WhenCallMethod_ThenUserDatabaseAddReceivedCall()
        {
            IUsersDatabase usersDatabase = Substitute.For<IUsersDatabase>();
        }
    }
}
