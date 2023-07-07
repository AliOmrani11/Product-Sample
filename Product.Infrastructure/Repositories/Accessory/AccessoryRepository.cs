using Product.Application.Repositories.Accessory;
using Product.Domain.Entities.Accessory;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories.Accessory;

public class AccessoryRepository : Repository<AccessoryEntity>, IAccessoryRepository
{
    public AccessoryRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}
