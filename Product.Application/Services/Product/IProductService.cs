using Product.Application.Dto.Request.Property;
using Product.Application.Dto.Response.Product;

namespace Product.Application.Services.Product;

public interface IProductService
{
    Task<List<GenerateProductResponse>> CombinationLists(List<GenerateProductResponse> oldList, List<ProductPropertyValueRequest> newList);
}
