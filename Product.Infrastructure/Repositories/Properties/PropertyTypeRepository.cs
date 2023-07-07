using Product.Application.Repositories.Properties;
using Product.Domain.Entities.Properties;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories.Properties;

public class PropertyTypeRepository : Repository<PropertyTypeEntity>, IPropertyTypeRepository
{
    public PropertyTypeRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}
