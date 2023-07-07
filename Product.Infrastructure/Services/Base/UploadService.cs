using Microsoft.AspNetCore.Http;
using Product.Application.Dto.Response.Base;
using Product.Application.Services.Base;

namespace Product.Infrastructure.Services.Base;

public class UploadService : IUploadService
{

    public async Task<LocalUploadResponse> UploadFile(IFormFile file)
    {
        if (file.Length == 0)
        {
            return new LocalUploadResponse
            {
                Success = false,
                Error = "No file found to upload.",
            };
        }
        string fileName = Path.GetFileName(file.FileName);
        string fileExtention = Path.GetExtension(fileName);
        string fileNewName = string.Format("{0}{1}", Guid.NewGuid().ToString(), fileExtention);
        string fullPath = Path.Combine("wwwroot/image/", fileNewName);
        string path = Path.Combine("/image/", fileNewName);
        if (!Directory.Exists("wwwroot/image"))
        {
            Directory.CreateDirectory("wwwroot/image");
        }

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return new LocalUploadResponse
        {
            Success = true,
            Url = path
        };
    }
}
