using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Application.Dto.Request.Accessory;
using Product.Application.Dto.Request.Property;
using Product.Application.Dto.Response.Public;

namespace Product.Application.Dto.Request.Product;

public class InsertProductRequest : IRequest<ServiceResult>
{
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
    public List<ProductPropertyRequest> Properties { get; set; }
    public List<ProductAccessoryRequest> Accessories { get; set; }
}

public class InsertProductValidator : AbstractValidator<InsertProductRequest>
{
    public InsertProductValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(250)
            .WithMessage("Name Maximum characters is 250");

        RuleFor(s => s.PhotoUrl)
            .NotEmpty().WithMessage("PhotoUrl is required");

        RuleFor(s => s.Properties).NotEmpty().WithMessage("Properties is required");

        RuleForEach(s => s.Properties).SetValidator(new ProductPropertyValidator());

        //RuleFor(s => s.Accessories).NotEmpty().WithMessage("Accessories is required");

        RuleForEach(s => s.Accessories).SetValidator(new ProductAccessoryValidator());
    }
}
