using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Application.Dto.Response.Base;

namespace Product.Application.Dto.Request.Base;

public class UploadRequest : IRequest<LocalUploadResponse>
{
    public IFormFile Photo { get; set; }
}

public class UploadValidator : AbstractValidator<UploadRequest>
{
    public UploadValidator()
    {
        RuleFor(s=>s.Photo).NotNull().WithMessage("Photo is required");
    }
}