using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Order.Api.Controllers.Customers;
using Order.Domain;
using Order.Domain.Customers;
using Order.Services.Customers;
using Xunit;
using Xunit.Abstractions;

namespace Order.Api.UnitTests.Controllers.Customers
{

    public class CustomerControllerTest
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService customerService;
        private readonly ICustomerMapper customerMapper;
        private readonly CustomerController customerController;

        public CustomerControllerTest()
        {
            _logger = Substitute.For<ILogger<CustomerController>>();
            customerService = Substitute.For<ICustomerService>();
            customerMapper = Substitute.For<ICustomerMapper>();
            customerController = new CustomerController(customerService,customerMapper,
                                                       _logger);
        }

        [Fact]
        public void GivenCustomerController_WhenAdminGiveExistingCustomerId_ThenGetCustomerDetailIsOk()
        {
            //Given
            Customer findCustomer = new Customer("ID");
            CustomerDtoOverView dtoFindCustomer = new CustomerDtoOverView();
            customerService.GetDetailCustomer("ID").Returns(findCustomer);
            customerMapper.CustomerDtoOverView(findCustomer).Returns(dtoFindCustomer);

            //when
            var checkOk = customerController.GetCustomerDetail("ID").Result;

            //then
            Assert.IsType<OkObjectResult>(checkOk);
        }

        [Fact]
        public void GivenCustomerController_WhenAdminGiveNotExistingCustomerId_ThenGetCustomerDetailBadRequest()
        {
            //Given
            Customer findCustomer = new Customer("ID");
            customerService.GetDetailCustomer("ID").Returns(x =>
            {
                throw new OrderExeptions("Error when not finding Customer on Id");
            });


            //when
            var checkOk = customerController.GetCustomerDetail("ID").Result;

            //then
            Assert.IsType<BadRequestObjectResult>(checkOk);
        }
    }
}
