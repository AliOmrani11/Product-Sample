using MediatR;
using Product.Application.Dto.Request.Product;
using Product.Application.Dto.Response.Public;
using Product.Application.UnitofWorks;
using System.Data;

namespace Product.Infrastructure.CQRS.Command.Product;

public class UpdateProductDiscountHandler : IRequestHandler<UpdateProductDiscountRequest, ServiceResult>
{

    private readonly IUnitofWork _dbContext;

    public UpdateProductDiscountHandler(IUnitofWork dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult> Handle(UpdateProductDiscountRequest request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.ProductGroupRepository.GetAsync(s => s.Id == request.Id);
        if (product == null)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Error = "id is wrong;"
            };
        }
        product.Discount = request.Discount;
        product.DiscountExpire = request.DiscountExpire;
        await _dbContext.SaveAsync();
        return new ServiceResult
        {
            IsSuccess = true
        };
    }
}
