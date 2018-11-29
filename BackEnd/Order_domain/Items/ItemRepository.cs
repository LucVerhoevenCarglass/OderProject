using Order_service.Data;

namespace Order_domain.Items
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly OrderContext _context;

        public ItemRepository(OrderContext context)
            : base(context)
        {
        }
    }
}
