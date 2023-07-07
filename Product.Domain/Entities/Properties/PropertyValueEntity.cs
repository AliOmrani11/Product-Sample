using Product.Domain.Common;
using Product.Domain.Entities.Product;

namespace Product.Domain.Entities.Properties;

public class PropertyValueEntity : BaseEntity
{
    public int PropertyTypeId { get; set; }
    public string Value { get; set; }



    public PropertyTypeEntity PropertyType { get; set; }

    public List<ProductPropertyEntity> ProductProperties { get; set; } = new List<ProductPropertyEntity>();
}
