using Product.Application.Repositories.Product;
using Product.Domain.Entities.Product;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repositories.Product;

public class ProductRepository : Repository<ProductEntity>, IProductRepository
{
    public ProductRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}
