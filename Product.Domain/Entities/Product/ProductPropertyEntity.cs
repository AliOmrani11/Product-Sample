using Product.Domain.Common;
using Product.Domain.Entities.Properties;

namespace Product.Domain.Entities.Product;

public class ProductPropertyEntity : BaseEntity
{
    public int ProductId { get; set; }
    public int PropertyValueId { get; set; }


    public ProductEntity Product { get; set; }
    public PropertyValueEntity PropertyValue { get; set; }
}
