using Order.Domain.Items;

namespace Order.Api.Controllers.Items
{
    public interface IItemMapper
    {
        ItemDtoOverView ItemDtoOverView(Item item);
        Item FromCreateItemDto_To_Item(ItemDtoToCreate itemDtoToCreate);
        ItemDtoToCreate FromItem_To_ItemDtoToCreate(Item item);
    }
}
