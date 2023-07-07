using Product.Infrastructure.Config;
using ProductSample.Api.Configuration.Config;
using ProductSample.Api.Configuration.Filters;
using ProductSample.Api.Configuration.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ApiResultFilter>();
});
builder.Services.RegisterService(builder.Configuration);
builder.Services.RegisterSetting();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/Site-v1/swagger.json", "Site");
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<ManageExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
