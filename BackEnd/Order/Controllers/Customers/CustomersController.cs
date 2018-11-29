using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Order_service.Customers;

namespace Order_api.Controllers.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerMapper _customerMapper;

        public CustomersController(ICustomerService customerService, CustomerMapper customerMapper)
        {
            _customerService = customerService;
            _customerMapper = customerMapper;
        }

        [HttpPost]
        public CustomerDto CreateCustomer([FromBody]CustomerDto customerDto)
        {
            return _customerMapper.ToDto(
                _customerService.CreateCustomer(
                    _customerMapper.ToDomain(customerDto)));
        }

        [HttpGet]
        public List<CustomerDto> GetAllCustomers()
        {
            return _customerService.GetAllCustomers().Select(customer => _customerMapper.ToDto(customer)).ToList();
        }
        
        [HttpGet("{id}")]
        public CustomerDto GetCustomer([FromRoute]string id)
        {
            return _customerMapper.ToDto(
                _customerService.GetCustomer(new Guid(id)));
        }
    }
}
