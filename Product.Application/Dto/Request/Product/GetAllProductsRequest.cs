using MediatR;
using Product.Application.Dto.Response.Product;

namespace Product.Application.Dto.Request.Product;

public class GetAllProductsRequest : IRequest<List<ProductGroupResponse>>
{
}
