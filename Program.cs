using Microsoft.EntityFrameworkCore;
using Gestao_Escala.Data;
using Gestao_Escala.Services;
using Gestao_Escala.Domain.Interfaces;
using System.Text.Json.Serialization;
using DotNetEnv;
using Gestao_Escala.Domain.interfaces;
using Gestao_Escala.interfaces;

Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IEscalaService, EscalaService>();
builder.Services.AddScoped<IMotoristaService, MotoristaService>();
builder.Services.AddScoped<IVigenciaService, VigenciaService>();
builder.Services.AddScoped<IVigenciaMotorista, VigenciaMotoristaService>();
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