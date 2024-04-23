using WebAPI.Entities;
using static WebAPI.Dtos;

namespace WebAPI
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item,string name,string description)
        {
            return new InventoryItemDto(item.CatalogItemId,name,description,item.Quantity,item.AcquiredDate);
        }
    }
}
