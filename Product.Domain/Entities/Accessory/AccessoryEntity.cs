using Product.Domain.Common;
using Product.Domain.Entities.Product;

namespace Product.Domain.Entities.Accessory;

public class AccessoryEntity : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public List<ProductGroupEntity> ProductGroups { get; set; } = new List<ProductGroupEntity>();
}
