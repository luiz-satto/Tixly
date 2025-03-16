using BuildingBlocks.Exceptions.Handler;
using Tixly.Application;
using Tixly.Domain;
using Tixly.Infrastructure;
using Tixly.Infrastructure.Data.Extensions;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDomainServices()
    .AddDomainServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseExceptionHandler(options => { });

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.InitialiseDatabase();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
