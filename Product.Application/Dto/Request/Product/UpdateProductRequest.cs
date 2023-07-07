using FluentValidation;
using MediatR;
using Product.Application.Dto.Response.Public;
using Product.Domain.Common.Enums;

namespace Product.Application.Dto.Request.Product;

public class UpdateProductRequest : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public string Price { get; set; }
    public PriceTypeEnum PriceType { get; set; }
    public decimal? Weight { get; set; }
    public int Inventory { get; set; }
}

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(s => s.Id).NotNull().WithMessage("Id is required.");
        RuleFor(s => s.Price).NotEmpty().WithMessage("Price is required.");
        RuleFor(s => s.PriceType).NotNull().WithMessage("PriceType is required.");
        RuleFor(s => s.Inventory).NotNull().WithMessage("Inventory is required.");
        RuleFor(s => s.Weight).NotNull().WithMessage("Weight is required.");
    }
}
