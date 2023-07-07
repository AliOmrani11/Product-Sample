using Microsoft.AspNetCore.Http;
using Product.Application.Dto.Response.Base;

namespace Product.Application.Services.Base;

public interface IUploadService
{
    Task<LocalUploadResponse> UploadFile(IFormFile file);
}
