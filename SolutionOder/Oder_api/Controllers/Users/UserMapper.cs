using Order.Domain.Users;

namespace Order.Api.Controllers.Users
{
    public class UserMapper: IUserMapper
    {
        public User DtoCreateNewUser(UserDtoToCreate userToCreate)
        {
            User returnUser = new User
                { Email = userToCreate.Email, Password = userToCreate.Password};
            userToCreate.Id = returnUser.UserId;

            returnUser.CheckUserValues();
            return returnUser;
        }

        public UserDtoOverView UserDtoOverView(User user)
        {
            return new UserDtoOverView
            {
                UserId = user.UserId,
                Email = user.Email                 
            };          
        }
    }
}
