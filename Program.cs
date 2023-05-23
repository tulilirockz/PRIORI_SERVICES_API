using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using PRIORI_SERVICES_API.Repository.Interface;
using PRIORI_SERVICES_API.Repository;
using PRIORI_SERVICES_API.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
              options.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();

builder.Services.AddDbContext<PrioriDbContext>(opt =>
{
    foreach (var item in typeof(EnvironmentVariables).GetFields())
    {
        if (item.GetValue(null) == null)
        {
            throw new ArgumentException($"Failure to run the API, check your whether your environment variables are set properly: Missing {item}");
        }
    }

    opt.UseSqlServer($"Server={EnvironmentVariables.PRIORI_DATABASE_IP},{EnvironmentVariables.PRIORI_DATABASE_PORT};DataBase={EnvironmentVariables.PRIORI_DATABASE_NAME};user id={EnvironmentVariables.PRIORI_DATABASE_USER};password={EnvironmentVariables.PRIORI_DATABASE_PASSWORD};Encrypt=True;TrustServerCertificate=True");
}
);

builder.Services.AddScoped<IAtualizacaoRepository, AtualizacaoRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opt.OperationFilter<SecurityRequirementsOperationFilter>();
}
);
builder.Services.AddAuthentication().AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.PRIORI_SECRET_JWT_KEY))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
