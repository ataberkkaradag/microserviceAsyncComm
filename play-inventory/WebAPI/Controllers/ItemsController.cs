using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using play_common;
using WebAPI.Clients;
using WebAPI.Entities;
using static WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> _itemsRepository;
        private readonly CatalogClient _catalogClient;

        public ItemsController(IRepository<InventoryItem> itemsrepository,CatalogClient catalogClient)
        {
            _itemsRepository = itemsrepository;
            _catalogClient = catalogClient;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest();

            }
            var catalogItems = await _catalogClient.GetCatalogItemsAsync();
            var inventoryItemEntities=await _itemsRepository.GetAllAsync(item=>item.UserId==userId);

            var inventoryItemDtos = inventoryItemEntities.Select(inventoryItem =>
            {
                var catalogtem = catalogItems.Single(catalogtem => catalogtem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.AsDto(catalogtem.Name, catalogtem.Description);
            });


           return Ok(inventoryItemDtos);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GrantItemsDto grantItemsDto)
        {
            var inventoryItem=await _itemsRepository.GetByIdAsync(item=>item.UserId==grantItemsDto.UserId && item.CatalogItemId==grantItemsDto.CatalogItemId);

            if(inventoryItem == null)
            {
                inventoryItem = new InventoryItem
                {
                    CatalogItemId = grantItemsDto.CatalogItemId,
                    UserId = grantItemsDto.UserId,
                    Quantity = grantItemsDto.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };
                await _itemsRepository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quantity += grantItemsDto.Quantity;
                await _itemsRepository.UpdateAsync(inventoryItem);
            }
            return Ok();

        }
    }
}
