using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain;
using Order.Domain.Users;
using Xunit;

namespace Order.Domain.UnitTests.Users
{
    public class UserTest
    {
        [Theory]
        [InlineData("Email","Password", "not a correct Email-format")]
        [InlineData(null, "Password", "Email is required")]
        public void GivenUserDatabase_WhenCreateNewUser_ThenEmailShouldBeValid(string email, string password, string errorMessage)
        {
            User newUser = new User()
            {
                Email = email,
                Password = password
            };

            var errorExeption = Assert.Throws<OrderExeptions>(
                                () => newUser.CheckUserValues());
            Assert.Contains(errorMessage, errorExeption.Message);
        }

        [Theory]
        [InlineData("Email@test.com", "Pass", "Password must contain at least")]
        [InlineData("Email@test.com", null, "Password is required")]
        [InlineData("Email@test.com", "Passwoordje", "The password is not valid. It should contain")]
        public void GivenUserDatabase_WhenCreateNewUser_ThenPasswordShouldBeValid(string email, string password, string errorMessage)
        {
            User newUser = new User()
            {
                Email = email,
                Password = password
            };

            var errorExeption = Assert.Throws<OrderExeptions>(
                () => newUser.CheckUserValues());
            Assert.Contains(errorMessage, errorExeption.Message);
        }
    }
}
