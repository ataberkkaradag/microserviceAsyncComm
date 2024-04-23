using MassTransit;
using Play.Catalog.Contracts;
using play_common;
using WebAPI.Entities;

namespace WebAPI.Consumers
{
    public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
    {

        private readonly IRepository<CatalogItem> _catalogItemRepository;
        public CatalogItemDeletedConsumer(IRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }
        public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
        {
            var message = context.Message;
            var item = await _catalogItemRepository.GetByIdAsync(message.ItemId);
            if (item == null)
            {
                return;
            }
            
            await _catalogItemRepository.RemoveAsync(message.ItemId);
            
        }
    }
}
