using Order_domain;
using Order_domain.Items;

namespace Order_service.Items
{
    public class ItemValidator : EntityValidator<Item>
    {
        protected override bool IsAFieldEmptyOrNull(Item item)
        {
            return IsNull(item)
                   || IsEmptyOrNull(item.Name)
                   || IsEmptyOrNull(item.Description)
                   || item.AmountOfStock < 0
                   || IsNull(item.Price)
                   || item.Price.GetAmountAsFloat() <= 0;
        }
    }
}
