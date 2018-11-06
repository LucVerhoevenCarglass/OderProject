using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Users;

namespace Order.Services.Users
{
    public interface IUserService
    {
        User CreateNewUser(User userToCreate);
        Task<User> Authenticate(string email, string password);
        IEnumerable<User> GetAllUsers();
    }
}
