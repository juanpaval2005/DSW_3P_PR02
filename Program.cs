using Microsoft.EntityFrameworkCore;
using DSW_3P_PR02.Data;
using DSW_3P_PR02.Repositories;
using DSW_3P_PR02.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//builder.Services.AddSingleton<IEventosRepository, EventosJsonRepository>();
builder.Services.AddScoped<IEventosRepository, EventosSqlRepository>();
builder.Services.AddScoped<IEventosService, EventosService>();

//Configuración del SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection")
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
