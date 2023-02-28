using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
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
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "JWTToken_Auth_API",
		Version = "v1"
	});
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
	AddSwaggerDocumentation(c);
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("General", options =>
	{
		options.TokenValidationParameters = new()
		{
			ValidateAudience = true,
			ValidateIssuer = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,

			ValidAudience = builder.Configuration["Token:Audience"],
			ValidIssuer = builder.Configuration["Token:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
			LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
		};
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy => policy.RequireClaim("userRole", "Admin"));
});

var app = builder.Build();


// Created CORS policy was used.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();


static void AddSwaggerDocumentation(SwaggerGenOptions o)
{
	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}