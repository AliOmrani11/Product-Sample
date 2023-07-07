using Product.Application.Repositories.Product;
using Product.Domain.Entities.Product;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories.Product;

public class ProductGroupRepository : Repository<ProductGroupEntity>, IProductGroupRepository
{
    public ProductGroupRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}
