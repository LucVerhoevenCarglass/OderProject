using Order.Api.Controllers.Users;
using Order.Domain.Customers;

namespace Order.Api.Controllers.Customers
{
    public interface ICustomerMapper
    {
        Customer DtoCreateNewCustomer(UserDtoToCreate customerToCreate);
        CustomerDtoOverView CustomerDtoOverView(Customer cust);
    }
}
