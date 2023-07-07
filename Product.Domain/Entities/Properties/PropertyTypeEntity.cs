using Product.Domain.Common;

namespace Product.Domain.Entities.Properties;

public class PropertyTypeEntity : BaseEntity
{
    public string Name { get; set; }


    public List<PropertyValueEntity> PropertyValues { get; set; } = new List<PropertyValueEntity>();
}
