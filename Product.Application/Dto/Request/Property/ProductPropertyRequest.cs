using FluentValidation;

namespace Product.Application.Dto.Request.Property;

public class ProductPropertyRequest
{
    public int PropertyTypeId { get; set; }

    public List<ProductPropertyValueRequest> Values { get; set; }
}

public class ProductPropertyValidator : AbstractValidator<ProductPropertyRequest>
{
    public ProductPropertyValidator()
    {
        RuleFor(s => s.PropertyTypeId).NotNull().WithMessage("PropertyTypeId is required");
        RuleFor(s => s.Values).NotEmpty().WithMessage("Values is required");
        RuleForEach(s => s.Values).SetValidator(new ProductPropertyValueValidator());
    }
}
