using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using ProductSample.Api.Configuration.MiddleWares;
using System.Reflection;

namespace ProductSample.Api.Configuration.Config;

public static class SettingConfiguration
{

    public static IServiceCollection RegisterSetting(this IServiceCollection services)
    {

        services.AddTransient<ManageExceptionHandlingMiddleware>();


        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt);
        });



        services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("Site-v1", new OpenApiInfo
            {
                Title = "Site-Api",
                Version = "v1",

            });
            //-------------------------------------------------------------------------------------------------------------------------
            string fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
            option.IncludeXmlComments(filePath);
            
        });



        return services;
    }
}
