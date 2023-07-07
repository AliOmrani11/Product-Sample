using Product.Application.Repositories.Properties;
using Product.Domain.Entities.Properties;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories.Properties;

public class PropertyValueRepository : Repository<PropertyValueEntity>, IPropertyValueRepository
{
    public PropertyValueRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}