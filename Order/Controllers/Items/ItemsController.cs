using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Order_service.Items;

namespace Order_api.Controllers.Items
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly ItemMapper _itemMapper;

        public ItemsController(IItemService itemService, ItemMapper itemMapper)
        {
            _itemService = itemService;
            _itemMapper = itemMapper;
        }

        [HttpPost]
        public ItemDto CreateItem([FromBody] ItemDto itemDto)
        {
            return _itemMapper.ToDto(
                _itemService.CreateItem(
                    _itemMapper.ToDomain(itemDto)));
        }

        [HttpPut("/{id}")]
        public ItemDto UpdateItem([FromRoute] string id, [FromBody] ItemDto itemDto)
        {
            return _itemMapper.ToDto(
                _itemService.UpdateItem(
                    _itemMapper.ToDomain(new Guid(id), itemDto)));
        }

        [HttpGet]
        public List<ItemDto> GetAllItems([FromQuery] string stockUrgency)
        {
            var allItems = _itemService.GetAllItems()
                                       .Select(x => _itemMapper.ToDto(x))
                                       .OrderBy(x => x.AmountOfStock);

            return FilterOnStockUrgency(stockUrgency, allItems).ToList();
        }

        private IEnumerable<ItemDto> FilterOnStockUrgency(string stockUrgency, IEnumerable<ItemDto> allItems)
        {
            if (stockUrgency != null)
            {
                return allItems.Where(item => item.StockUrgency == stockUrgency);
            }

            return allItems;
        }
    }
}
