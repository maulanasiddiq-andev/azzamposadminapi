using AzposAdminApi.Extensions;
using AzposAdminApi.Models;
using AzposAdminApi.Services;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var azposAdminApiConnectionString = builder.Configuration.GetConnectionString("AzzamPOSPostgreSQL");
builder.Services.AddDbContext<AzPosDBContext>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.UseNpgsql(azposAdminApiConnectionString);
    options.UseExceptionProcessor();
});

var activitylogConnectionString = builder.Configuration.GetConnectionString("ActivityLogPostgreSQL");
builder.Services.AddDbContext<ActivityLogDBContext>(options =>
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    options.UseNpgsql(activitylogConnectionString);
});

// Register Service Extensions
builder.Services.RegisterRepositories();

builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
    
//     // Add Swagger UI pointing to the OpenAPI JSON endpoint
//     app.UseSwaggerUI(options =>
//     {
//         options.SwaggerEndpoint("/openapi/v1.json", "AzposAdminApi v1");
//     });
// }

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "AzposAdminApi v1");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
