using Newtonsoft.Json;
using Product.Application.Dto.Response.Public;
using System.ComponentModel.DataAnnotations;

namespace ProductSample.Api.Configuration.MiddleWares;

public class ManageExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(JsonConvert.SerializeObject(e));
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);
        var response = new ApiBaseResult { Error = true, Message = new List<string>() { exception.Message } };
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(response.ToString());
    }
    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            //BadRequestException => StatusCodes.Status400BadRequest,
            //NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
}
