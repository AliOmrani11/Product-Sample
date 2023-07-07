using MediatR;
using Product.Application.Dto.Request.Base;
using Product.Application.Dto.Response.Base;
using Product.Application.Services.Base;

namespace Product.Infrastructure.CQRS.Command.Base;

public class UploadHandler : IRequestHandler<UploadRequest, LocalUploadResponse>
{

    private readonly IUploadService _uploadService;

    public UploadHandler(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    public async Task<LocalUploadResponse> Handle(UploadRequest request, CancellationToken cancellationToken)
    {
        return  await _uploadService.UploadFile(request.Photo);
    }
}
