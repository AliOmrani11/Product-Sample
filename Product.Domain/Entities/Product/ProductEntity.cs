using Product.Domain.Common;
using Product.Domain.Common.Enums;

namespace Product.Domain.Entities.Product;

public class ProductEntity : BaseEntity
{

    public string Name { get; set; }
    public PriceTypeEnum? PriceType { get; set; }
    public string Price { get; set; }
    public decimal? Weight { get; set; }
    public int? Inventory { get; set; }
    public int ProductGroupId { get; set; }
    public string PropertyJson { get; set; }


    public ProductGroupEntity ProductGroup { get; set; }

    public List<ProductPropertyEntity> ProductProperties { get; set; } = new List<ProductPropertyEntity>();

}
