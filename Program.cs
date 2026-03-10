using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Data;
using Gestao_Escala.Services;
using Gestao_Escala.Domain.Interfaces;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configura PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// --- ADICIONE ESTA LINHA AQUI ---
builder.Services.AddScoped<IEscalaService, EscalaService>();
// --------------------------------

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
