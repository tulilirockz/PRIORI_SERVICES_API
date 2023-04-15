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
    var database_args = new Dictionary<string, string?> {
        { "port", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_PORT") ?? "1433" },
        { "ip", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_IP") ?? "localhost"},
        { "name", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_NAME") ?? "Priori"},
        { "user", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_USER")},
        { "pass", System.Environment.GetEnvironmentVariable("PRIORI_DATABASE_PASSWORD") }
    };

    foreach (string? item in database_args.Values)
    {
        if (item == null)
        {
            throw new ArgumentException("Failure to run the API, check your whether your environment variables are set properly.");
        }
    }

    opt.UseSqlServer($"{database_args["ip"]},{database_args["port"]};DataBase={database_args["name"]};user id={database_args["user"]};password={database_args["pass"]},trusted_connection=true");
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTKey").Value!))
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
