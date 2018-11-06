using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Order.Services.Items;

namespace Order.Api.Controllers.Items
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IItemMapper _itemMapper;
        private readonly ILogger<ItemController> _logger;
        public ItemController(IItemService itemService, IItemMapper itemMapper,
                              ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _itemMapper = itemMapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IEnumerable<ItemDtoOverView> Get()
        {
            return _itemService.GetAllItems().Select(item => _itemMapper.ItemDtoOverView(item));
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult<ItemDtoToCreate> CreateNewItem([FromBody] ItemDtoToCreate itemDtoToCreate)
        {
            try
            {
                var item = _itemService.CreateNewItem(_itemMapper.FromCreateItemDto_To_Item(itemDtoToCreate));
                return Ok(_itemMapper.FromItem_To_ItemDtoToCreate(item));
            }
            catch (OrderExeptions itemEx)
            {
                return Logexeption(itemEx.Message);
            }
            catch (Exception ex)
            {
                return Logexeption(ex.Message);
            }
        }

        private ActionResult Logexeption(string itemExMessage)
        {
            _logger.LogError(itemExMessage);
            return BadRequest(itemExMessage);
        }
    }
}