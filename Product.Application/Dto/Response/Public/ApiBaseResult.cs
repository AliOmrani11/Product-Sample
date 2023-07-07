using Newtonsoft.Json;

namespace Product.Application.Dto.Response.Public;

public class ApiBaseResult
{
    public ApiBaseResult(bool error, string? message = null)
        : this()
    {
        Error = error;
        Message.Add(message ?? "OK");
    }
    public ApiBaseResult()
    {
        Message = new List<string>();
    }


    public bool Error { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Message { get; set; }



    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
