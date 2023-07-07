using MediatR;
using Product.Application.Dto.Request.Product;
using Product.Application.Dto.Response.Public;
using Product.Application.UnitofWorks;
using System.Data;

namespace Product.Infrastructure.CQRS.Command.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, ServiceResult>
{

    private readonly IUnitofWork _dbContext;

    public UpdateProductHandler(IUnitofWork dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.ProductRepository.GetAsync(s => s.Id == request.Id);
        if (product == null)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Error = "id is wrong;"
            };
        }
        product.Price = request.Price;
        product.Inventory = request.Inventory;
        product.Weight = request.Weight;
        product.PriceType = request.PriceType;
        await _dbContext.SaveAsync();
        return new ServiceResult
        {
            IsSuccess = true
        };

    }
}
