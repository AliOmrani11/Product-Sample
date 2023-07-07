using FluentValidation;

namespace Product.Application.Dto.Request.Property;

public class ProductPropertyValueRequest
{
    public int Id { get; set; }
    public string Value { get; set; }
}

public class ProductPropertyValueValidator : AbstractValidator<ProductPropertyValueRequest>
{
    public ProductPropertyValueValidator()
    {
        RuleFor(s => s.Id).NotNull().WithMessage("Id is required");
        RuleFor(s => s.Value).NotEmpty().WithMessage("Value is required");
    }
}