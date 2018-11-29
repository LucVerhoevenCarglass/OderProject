using Order_domain;
using Order_domain.Customers;

namespace Order_service.Customers
{
    public class CustomerValidator : EntityValidator<Customer>
    {
        protected override bool IsAFieldEmptyOrNull(Customer customer)
        {
            return IsNull(customer)
                   || IsEmptyOrNull(customer.FirstName)
                   || IsEmptyOrNull(customer.LastName)
                   || IsNull(customer.Address)
                   || IsEmptyOrNull(customer.Address.StreetName)
                   || IsEmptyOrNull(customer.Address.HouseNumber)
                   || IsEmptyOrNull(customer.Address.PostalCode)
                   || IsEmptyOrNull(customer.Address.Country)
                   || IsNull(customer.Email)
                   || IsEmptyOrNull(customer.Email.Domain)
                   || IsEmptyOrNull(customer.Email.LocalPart)
                   || IsNull(customer.PhoneNumber)
                   || IsEmptyOrNull(customer.PhoneNumber.CountryCallingCode)
                   || IsEmptyOrNull(customer.PhoneNumber.Number);
        }

    }
}
