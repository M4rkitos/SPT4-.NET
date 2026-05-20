using Microsoft.EntityFrameworkCore;
using EasyAccess.Infrastructure.Data;
using EasyAccess.Infrastructure.Repositories;
using EasyAccess.Application.Services;
using EasyAccess.Domain.Repositories;
using EasyAccess.Api.Middleware;
using Serilog;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DO SERILOG ---
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/easyaccess-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// --- SERVIÇOS PADRÃO ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 

builder.Services.AddAuthorization();

// --- CONFIGURAÇÃO DO BANCO EM MEMÓRIA GENÉRICO ---
builder.Services.AddDbContext<EasyAccessDbContext>(options =>
    options.UseInMemoryDatabase("EasyAccessDbLocal"));

// --- INJEÇÃO DE DEPENDÊNCIA ---
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<IVagaService, VagaService>();

// --- HEALTH CHECKS ---
builder.Services.AddHealthChecks();

// --- OPENTELEMETRY ---
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation() 
        .AddConsoleExporter());

var app = builder.Build();

// --- ATIVAÇÃO DO MIDDLEWARE GLOBAL DE EXCEÇÕES ---
app.UseMiddleware<GlobalExceptionMiddleware>();

// --- MIDDLEWARES ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    c.RoutePrefix = string.Empty; // Define o Swagger como página inicial
});

app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();