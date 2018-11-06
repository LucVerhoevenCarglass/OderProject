using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Order.Domain.Customers;
using Order.Services.Customers;

namespace Order.Api.Controllers.Customers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerMapper _customerMapper;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService,
            ICustomerMapper customerMapper,
            ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _customerMapper = customerMapper;
            _logger = logger;
        }

        // GET: api/Customer
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<CustomerDtoOverView> Get()
        {
            return _customerService.GetAllCustomers().Select(cust => _customerMapper.CustomerDtoOverView(cust));
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "Get")]
        [Authorize(Policy = "Admin")]
        public ActionResult<CustomerDtoOverView> GetCustomerDetail(string id)
        {
                try
                {
                    Customer showCustomer = _customerService.GetDetailCustomer(id);
                    return Ok(_customerMapper.CustomerDtoOverView(showCustomer));
                }
                catch (OrderExeptions customerEx)
                {
                    return Logexeption(customerEx.Message);
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



//// POST: api/Customer
//[HttpPost]
//public void Post([FromBody] string value)
//{
//}

//// PUT: api/Customer/5
//[HttpPut("{id}")]
//public void Put(int id, [FromBody] string value)
//{
//}

//// DELETE: api/ApiWithActions/5
//[HttpDelete("{id}")]
//public void Delete(int id)
//{
//}
//    }
//}
