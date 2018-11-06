using Order.Api.Controllers.Users;
using Order.Domain.Customers;

namespace Order.Api.Controllers.Customers
{
    public class CustomerMapper: ICustomerMapper
    {
        public Customer DtoCreateNewCustomer(UserDtoToCreate customerToCreate)
        {
            Customer newCustomer =  new Customer(customerToCreate.Id)
            {
                FirstName = customerToCreate.FirstName,
                LastName = customerToCreate.LastName,
                Email = customerToCreate.Email,
                StreetAndNumber= customerToCreate.StreetAndNumber,
                Zip= customerToCreate.Zip,
                City= customerToCreate.City,
                Telephone= customerToCreate.Telephone
            };
            newCustomer.CheckCustomerValues();
            return newCustomer;
        }

        public CustomerDtoOverView CustomerDtoOverView(Customer cust)
        {
            return new CustomerDtoOverView
            {
                CustomerId = cust.CustomerId,
                Email = cust.Email,
                Name = string.Concat(cust.LastName, ", ", cust.FirstName),
                Telephone = cust.Telephone
            };
        }
    }
}
