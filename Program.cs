using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PrioriDbContext>(opt =>
    opt.UseSqlServer(
        builder.Configuration["ConnectionString"]
    )
);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
