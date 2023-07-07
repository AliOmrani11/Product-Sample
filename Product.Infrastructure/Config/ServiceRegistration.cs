using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Services.Base;
using Product.Application.Services.Product;
using Product.Application.UnitofWorks;
using Product.Infrastructure.CQRS.Behaviours;
using Product.Infrastructure.Data;
using Product.Infrastructure.Services.Base;
using Product.Infrastructure.Services.Product;
using Product.Infrastructure.UnitofWorks;
using System.Reflection;

namespace Product.Infrastructure.Config;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });


        services.AddScoped<IUnitofWork, UnitofWork>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IUploadService, UploadService>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        var a = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(s=>s.FullName.Contains("Product.Application"));
        services.AddValidatorsFromAssembly(a, ServiceLifetime.Transient);
        //services.AddAutoMapper(
        //(serviceProvider, mapperConfiguration) =>
        // mapperConfiguration.AddProfile(new MapperProfile(configuration)), Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSingleton<IConfiguration>(configuration);

        // Configure IMapper
        services.AddSingleton<IMapper>(provider =>
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile(provider.GetRequiredService<IConfiguration>()));
            });

            return mapperConfiguration.CreateMapper();
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        return services;
    }
}
