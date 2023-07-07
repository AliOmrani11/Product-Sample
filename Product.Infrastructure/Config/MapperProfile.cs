using AutoMapper;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Product.Application.Dto.Response.Accessory;
using Product.Application.Dto.Response.Product;
using Product.Domain.Entities.Accessory;
using Product.Domain.Entities.Product;
using System.Data;

namespace Product.Infrastructure.Config;

public class MapperProfile : Profile
{
    public MapperProfile(IConfiguration configuration)
    {
        CreateMap<ProductPropertyEntity, ProductPropertyResponse>()
            .ConstructUsing(s => new ProductPropertyResponse
            {
                Id = s.Id,
                Name = s.PropertyValue.PropertyType.Name,
                Value = s.PropertyValue.Value
            });

        CreateMap<ProductGroupEntity, ProductGroupResponse>()
            .ConstructUsing(s => new ProductGroupResponse
            {
                DiscountPrice = s.DiscountExpire < DateTime.Now ? 0 : s.Discount ?? 0
            }).ForMember(s => s.Products, s => s.MapFrom(f => f.Products))
            .ForMember(s => s.Accessories, s => s.MapFrom(f => f.Accessories))
            .AfterMap((source, dest) =>
            {
                dest.Products = dest.Products.OrderByDescending(s => s.Amount).ToList();
            });

        CreateMap<ProductEntity, ProductResponse>()
            .AfterMap((source , dest) =>
            {
                decimal amount = Convert.ToDecimal(new DataTable().Compute(source.Price.Replace("$DOLLAR",configuration.GetSection("Setting:$DOLLAR").Value), ""));
                amount +=source.ProductGroup.Accessories.Sum(s => s.Price);
                dest.Amount = amount - source.ProductGroup.Discount??0;
                dest.Amount = dest.Amount >= 0 ? dest.Amount : 0;
            })
            .ForMember(s=>s.Properties , s=>s.MapFrom(f=>f.ProductProperties));


        CreateMap<AccessoryEntity, AccessoryResponse>();
    }
}
