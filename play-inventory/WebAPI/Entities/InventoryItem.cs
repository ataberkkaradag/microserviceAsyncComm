using play_common;

namespace WebAPI.Entities
{
    public class CatalogItem :IBaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
      
        
    }
}
