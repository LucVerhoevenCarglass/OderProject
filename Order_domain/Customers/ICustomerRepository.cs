using System;
using System.Collections.Generic;

namespace Order_domain.Customers
{
    public interface ICustomerRepository
    {
        Customer Save(Customer entity);

        Customer Update(Customer entity);

        Dictionary<Guid, Customer> GetAll();

        Customer Get(Guid entityId);
        
        void Reset();
    }
}