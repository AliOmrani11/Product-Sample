using Newtonsoft.Json;

namespace Product.Application.Dto.Response.Public;

public class ApiResult<TData> : ApiBaseResult
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public TData? Data { get; set; }
    public ApiResult(bool error, TData? data, string? message = null)
   : base(error, message)
    {
        Data = data;
    }

}
