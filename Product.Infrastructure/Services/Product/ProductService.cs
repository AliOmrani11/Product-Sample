using Product.Application.Dto.Request.Property;
using Product.Application.Dto.Response.Product;
using Product.Application.Services.Product;

namespace Product.Infrastructure.Services.Product;

public class ProductService : IProductService
{
    public async Task<List<GenerateProductResponse>> CombinationLists(List<GenerateProductResponse> oldList, List<ProductPropertyValueRequest> newList)
    {
        return (from m in oldList
                from s in newList
                select new GenerateProductResponse{ Id = m.Id+"," + s.Id , Value = m.Value + " " + s.Value }).ToList();
    }
}
