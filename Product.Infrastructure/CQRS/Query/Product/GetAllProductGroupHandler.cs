using AutoMapper;
using MediatR;
using Product.Application.Dto.Request.Product;
using Product.Application.Dto.Response.Product;
using Product.Application.UnitofWorks;

namespace Product.Infrastructure.CQRS.Query.Product
{
    public class GetAllProductGroupHandler : IRequestHandler<GetAllProductsRequest, List<ProductGroupResponse>>
    {

        private readonly IUnitofWork _dbContext;
        private readonly IMapper _mapper;

        public GetAllProductGroupHandler(IUnitofWork dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ProductGroupResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.ProductGroupRepository.GetAllAsync(null, "Products.ProductProperties.PropertyValue.PropertyType,Accessories");

            var result = _mapper.Map<List<ProductGroupResponse>>(products);

            return result;
        }
    }
}
