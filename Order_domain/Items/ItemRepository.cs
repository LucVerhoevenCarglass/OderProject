namespace Order_domain.Items
{
    public class ItemRepository : Repository<Item, ItemDatabase>, IItemRepository
    {

        public ItemRepository(ItemDatabase database)
            : base(database)
        {
        }
    }
}
