using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Product.Application.Dto.Response.Public;

namespace ProductSample.Api.Configuration.Filters;

public class ApiResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is OkObjectResult okObjectResult)
        {
            var apiResult = new ApiResult<object>(false, okObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
        }
        else if (context.Result is OkResult okResult)
        {
            var apiResult = new ApiBaseResult(false);
            context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
        }
        //return BadRequest() method create an ObjectResult with StatusCode 400 in recent versions, So the following code has changed a bit.
        else if (context.Result is ObjectResult badRequestObjectResult && badRequestObjectResult.StatusCode == 400)
        {
            string message = "";
            switch (badRequestObjectResult.Value)
            {
                case ValidationProblemDetails validationProblemDetails:
                    var errorMessages = validationProblemDetails.Errors.SelectMany(p => p.Value).Distinct();
                    message = string.Join(" | ", errorMessages);
                    break;
                case SerializableError errors:
                    var errorMessages2 = errors.SelectMany(p => (string[])p.Value).Distinct();
                    message = string.Join(" | ", errorMessages2);
                    break;
                case var value when value != null && !(value is ProblemDetails):
                    if (badRequestObjectResult.Value is List<string>)
                        message = ((List<string>)badRequestObjectResult.Value).FirstOrDefault() ?? "Error";
                    else
                        message = (badRequestObjectResult.Value ?? "Error").ToString() ?? "Error";
                    break;
            }

            var apiResult = new ApiBaseResult(true, message);
            context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
        }
        else if (context.Result is ObjectResult notFoundObjectResult && notFoundObjectResult.StatusCode == 404)
        {
            string message = "";
            if (notFoundObjectResult.Value != null && !(notFoundObjectResult.Value is ProblemDetails))
                message = (notFoundObjectResult.Value ?? "Error").ToString() ?? "Error";

            //var apiResult = new ApiResults<object>(false, ApiResultStatusCode.NotFound, notFoundObjectResult.Value);
            var apiResult = new ApiBaseResult(true, message);
            context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
        }
        else if (context.Result is ContentResult contentResult)
        {
            var apiResult = new ApiBaseResult(false, contentResult.Content);
            context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
        }
        else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                                                             && !(objectResult.Value is ApiBaseResult))
        {
            var apiResult = new ApiResult<object>(false, objectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
        }
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {

    }
}
