using FluentValidation;
using MediatR;
using Product.Application.Dto.Response.Public;

namespace Product.Application.Dto.Request.Product;

public class UpdateProductDiscountRequest : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public decimal Discount { get; set; }
    public DateTime? DiscountExpire { get; set; }

}

public class UpdateProductDiscountValidator : AbstractValidator<UpdateProductDiscountRequest>
{
    public UpdateProductDiscountValidator()
    {
        RuleFor(s => s.Id).NotNull().WithMessage("Id is required.");
        RuleFor(s => s.Discount).NotNull().WithMessage("Discount is required.");

    }
}
