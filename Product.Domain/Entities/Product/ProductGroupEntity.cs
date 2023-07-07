using Product.Domain.Common;
using Product.Domain.Entities.Accessory;

namespace Product.Domain.Entities.Product;

public class ProductGroupEntity : BaseEntity
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public decimal? Discount { get; set; }
    public DateTime? DiscountExpire { get; set; }


    public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
    public List<AccessoryEntity> Accessories { get; set; } = new List<AccessoryEntity>();
}
