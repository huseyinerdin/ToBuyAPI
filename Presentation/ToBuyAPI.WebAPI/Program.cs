using FluentValidation.AspNetCore;
using System.Reflection;
using ToBuyAPI.Application.AutoMapper;
using ToBuyAPI.Infrastructure;
using ToBuyAPI.Infrastructure.Filters;
using ToBuyAPI.Infrastructure.Services.Storage.Local;
using ToBuyAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddStorage<LocalStorage>();

// CORS policy created.
builder.Services.AddCors(
    options => options.AddDefaultPolicy(
        policy => policy.WithOrigins("https://localhost:7156/", "http://localhost:7156/").AllowAnyHeader().AllowAnyMethod()));

// Validation filter added for validation checks on the backend.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
// TODO=>.AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Buraya valýdatorlardan býrýnýn adý gelmesý yeter>()).;

// Default validation check disabled
// TODO=>.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Created CORS policy was used.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
