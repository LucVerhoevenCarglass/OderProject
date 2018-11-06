using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Order.Api.Controllers.Customers;
using Order.Api.Controllers.Users;
using Order.Domain;
using Order.Domain.Users;
using Order.Services.Customers;
using Order.Services.Users;
using Xunit;
using Xunit.Abstractions;

namespace Order.Api.UnitTests.Controllers.Users
{

    public class UserControllerTest
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService userService;
        private readonly IUserMapper userMapper;
        private readonly ICustomerService customerService;
        private readonly ICustomerMapper customerMapper;
        private readonly UserController userController;

        public UserControllerTest()
        {
            _logger = Substitute.For<ILogger<UserController>>();
            userService = Substitute.For<IUserService>();
            userMapper = Substitute.For<IUserMapper>();
            customerService = Substitute.For<ICustomerService>();
            customerMapper = Substitute.For<ICustomerMapper>();
            userController = new UserController(userService, customerService,
                userMapper, customerMapper,
                _logger);
        }
        //public UserControllerTest(ITestOutputHelper output)
        //{
        //    _output = output;
        //    _logger = output.BuildLoggerFor<UserController>();
        //     userService = Substitute.For<IUserService>();
        //     userMapper = Substitute.For<IUserMapper>();
        //     customerService = Substitute.For<ICustomerService>();
        //     customerMapper = Substitute.For<ICustomerMapper>();
        //     userController = new UserController(userService, customerService,
        //        userMapper, customerMapper,
        //        _logger);
        //}

        [Fact]
        public void GivenUserController_WhenCreateNewUser_ThenReturnOk()
        {
            //Given
            UserDtoToCreate testUser= new UserDtoToCreate();
            User user = new User()
            {
                Email="Email@test.com",
                Password ="12Password3"
            };
            userMapper.DtoCreateNewUser(testUser).Returns(user);
            userService.CreateNewUser(user).Returns(user);

            //when
            var checkOk = userController.RegisterNewUser(testUser).Result;

            //then
            Assert.IsType<OkResult>(checkOk);
        }

        [Fact]
        public void GivenUserController_WhenCreateNewUserWithBadInput_ThenReturnBadRequest()
        {
            //Given
            UserDtoToCreate testUser = new UserDtoToCreate();
            User user = new User();
            userMapper.DtoCreateNewUser(testUser).Returns(x =>
            {
                throw new OrderExeptions("Error");
            });


            //when
            var checkOk = userController.RegisterNewUser(testUser).Result;

            //then
            Assert.IsType<BadRequestObjectResult>(checkOk);
        }

    }
}
