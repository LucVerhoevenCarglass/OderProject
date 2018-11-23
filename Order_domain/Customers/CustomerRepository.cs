using Order_domain.Orders;
using Order_service.Data;

namespace Order_domain.Customers
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly OrderContext _context;
        private CustomerRepository() { }

        public CustomerRepository(OrderContext context)
            : base(context)
        {
        }
    }
}
