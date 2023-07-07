namespace Product.Application.Dto.Response.Base;

public class LocalUploadResponse
{
    public string? Url { get; set; }
    public bool Success { get; set; }
    public string? Error { get; set; }
}
