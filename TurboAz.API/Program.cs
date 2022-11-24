using AspNetCoreRateLimit;
using MailKit;
using Microsoft.Extensions.Configuration;
using TurboAz.API.Infrastructure;
using TurboAz.Core.Models;
using TurboAz.Repository.CQRS.Commands.Abstract;
using TurboAz.Repository.CQRS.Commands.Concrete;
using TurboAz.Repository.CQRS.Queries.Abstract;
using TurboAz.Repository.CQRS.Queries.Concrete;
using TurboAz.Repository.Repositories.Abstract;
using TurboAz.Repository.Repositories.Concrete;
using TurboAz.Service.Extensions;
using TurboAz.Service.Infrastructure;
using TurboAz.Service.Services.Abstract;
using TurboAz.Service.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddProjectDependencies(builder.Configuration);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseMiddleware<RateLimitingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.UseRateLimiting();

app.Run();
