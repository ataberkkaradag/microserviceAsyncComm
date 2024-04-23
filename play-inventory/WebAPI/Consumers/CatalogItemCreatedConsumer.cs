﻿using MassTransit;
using Play.Catalog.Contracts;
using play_common;
using WebAPI.Entities;

namespace WebAPI.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
    {
        private readonly IRepository<CatalogItem> _catalogItemRepository;
        public CatalogItemCreatedConsumer(IRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }
        public async Task Consume(ConsumeContext<CatalogItemCreated> context)
        {
            var message=context.Message;
            var item = await _catalogItemRepository.GetByIdAsync(message.ItemId);
            if (item != null) 
            {
                return;
            }
            item=new CatalogItem {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description,
            };
            await _catalogItemRepository.CreateAsync(item);
        }
    }
}
