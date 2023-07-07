using Product.Application.Repositories.Product;
using Product.Domain.Entities.Product;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories.Product;

public class ProductPropertyRepository : Repository<ProductPropertyEntity>, IProductPropertyRepository
{
    public ProductPropertyRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}