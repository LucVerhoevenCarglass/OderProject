using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.Domain.Users;

namespace Order.Api.Controllers.Users
{
    public interface IUserMapper
    {
        User DtoCreateNewUser(UserDtoToCreate userToCreate);
        UserDtoOverView UserDtoOverView(User user);
    }
}
