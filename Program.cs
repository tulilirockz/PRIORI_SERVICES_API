using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using PRIORI_SERVICES_API.Repository.Interface;
using PRIORI_SERVICES_API.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
              options.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();

builder.Services.AddDbContext<PrioriDbContext>(opt =>
{
    var database_vars = new Dictionary<string, string?> {
        { "port", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_PORT") ?? "1433" },
        { "ip", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_IP") ?? "localhost"},
        { "name", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_NAME") ?? "Priori"},
        { "user", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_USER")},
        { "pass", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_PASSWORD") }
    };

    foreach (string? item in database_vars.Values)
    {
        if (item == null)
        {
            throw new ArgumentException("Failure to run the API, check your whether your environment variables are set properly.");
        }
    }

    opt.UseSqlServer($"Server={database_vars["ip"]},{database_vars["port"]};database={database_vars["name"]};user id={database_vars["user"]};password={database_vars["pass"]};Encrypt=True;TrustServerCertificate=True");
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(System.Environment.GetEnvironmentVariable("PRIORI_SECRET_JWT_KEY")! ?? "5UP3r53Cr37K3Y4M06U55U55Y84115"))
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
