using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PrioriDbContext>(opt => 
    opt.UseSqlServer(
        "Server=192.168.15.14,1433;DataBase=Priori;user id=sa;password=_ScoobyDooby23;Encrypt=True;TrustServerCertificate=True"
    )
);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
