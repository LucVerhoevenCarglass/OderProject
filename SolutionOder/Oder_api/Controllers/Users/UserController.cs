using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Api.Controllers.Customers;
using Order.Domain;
using Order.Domain.Customers;
using Order.Domain.Users;
using Order.Services.Customers;
using Order.Services.Users;

namespace Order.Api.Controllers.Users
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;
        private readonly ICustomerService _customerService;
        private readonly ICustomerMapper _customerMapper;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ICustomerService customerService,
                              IUserMapper userMapper, ICustomerMapper customerMapper,
                              ILogger<UserController> logger)
        {
            _userService = userService;
            _customerService = customerService;
            _userMapper = userMapper;
            _customerMapper = customerMapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<UserDtoOverView> Get()
        {
            return _userService.GetAllUsers().Select(user => _userMapper.UserDtoOverView(user));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<User> RegisterNewUser([FromBody] UserDtoToCreate userDtoToCreate)
        {
            try
            {
                User userToCreate = _userMapper.DtoCreateNewUser(userDtoToCreate);
                Customer customerToCreate = _customerMapper.DtoCreateNewCustomer(userDtoToCreate);
                _userService.CreateNewUser(userToCreate);
                _customerService.CreateNewCustomer(customerToCreate);
                return Ok();
            }
            catch (OrderExeptions userEx)
            {
                return Logexeption(userEx.Message);
            }
            catch (Exception ex)
            {
                return Logexeption(ex.Message);
            }
        }

        private ActionResult Logexeption(string itemExMessage)
        {
            _logger.LogError(itemExMessage);
            return BadRequest(itemExMessage);
        }
    }
}
