using MediatR;
using Product.Application.Dto.Request.Product;
using Product.Application.Dto.Response.Product;
using Product.Application.Dto.Response.Public;
using Product.Application.Services.Base;
using Product.Application.Services.Product;
using Product.Application.UnitofWorks;
using Product.Domain.Entities.Product;

namespace Product.Infrastructure.CQRS.Command.Product;

public class InsertProductHandler : IRequestHandler<InsertProductRequest, ServiceResult>
{

    private readonly IUnitofWork _dbContext;
    private readonly IUploadService _uploadService;
    private readonly IProductService _product;

    public InsertProductHandler(IUnitofWork dbContext, IUploadService uploadService, IProductService product)
    {
        _dbContext = dbContext;
        _uploadService = uploadService;
        _product = product;
    }

    public async Task<ServiceResult> Handle(InsertProductRequest request, CancellationToken cancellationToken)
    {

        ProductGroupEntity productGroup = new()
        {
            Name = request.Name,
            PhotoUrl = request.PhotoUrl
        };
        if (request.Accessories != null && request.Accessories.Any())
        {
            var accessories = await _dbContext.AccessoryRepository.GetManyAsync(s => request.Accessories.Select(f => f.Id).Contains(s.Id));
            productGroup.Accessories.AddRange(accessories.Distinct().ToList());
        }
        //Generate Products
        var groupJoin = (from req in request.Properties[0].Values
                         select new GenerateProductResponse { Id = req.Id.ToString(), Value = req.Value }).ToList();
        request.Properties.Remove(request.Properties[0]);
        foreach (var item in request.Properties)
        {
            groupJoin = await _product.CombinationLists(groupJoin, item.Values);
        }
        List<ProductEntity> products = new List<ProductEntity>();
        foreach (var item in groupJoin)
        {
            products.Add(new ProductEntity
            {
                Name = request.Name + " " + item.Value,
                Price = "0",
                PriceType = Domain.Common.Enums.PriceTypeEnum.CONSTANT,
                Weight = 0,
                Inventory = 0,
                ProductProperties = item.Id.Split(',').Select(s => new ProductPropertyEntity
                {
                    PropertyValueId = int.Parse(s)
                }).ToList()
            });
        }
        productGroup.Products.AddRange(products);
        await _dbContext.ProductGroupRepository.InsertAsync(productGroup);
        await _dbContext.SaveAsync();
        return new ServiceResult
        {
            IsSuccess = true
        };
    }
}
