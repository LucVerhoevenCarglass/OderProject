namespace Order_domain.Customers
{
    public class CustomerRepository : Repository<Customer, CustomerDatabase>, ICustomerRepository
    {
        public CustomerRepository(CustomerDatabase database)
            : base(database)
        {
        }
    }
}
