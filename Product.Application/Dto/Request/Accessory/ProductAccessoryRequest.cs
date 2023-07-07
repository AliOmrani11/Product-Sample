using FluentValidation;

namespace Product.Application.Dto.Request.Accessory;

public class ProductAccessoryRequest
{
    public int Id { get; set; }
}

public class ProductAccessoryValidator : AbstractValidator<ProductAccessoryRequest>
{
    public ProductAccessoryValidator()
    {
        RuleFor(s => s.Id).NotNull().WithMessage("Id is required");
    }
}
