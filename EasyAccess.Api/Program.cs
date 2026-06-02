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

// 💡 FORÇA A API A ESCUTAR A PORTA DA AZURE
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

// --- CONFIGURAÇÃO DO SERILOG ---
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/easyaccess-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// --- SERVIÇOS DE SEGURANÇA E INFRAESTRUTURA (Ordem Corrigida) ---
builder.Services.AddAuthorization(); // ◄ Deve ser um dos primeiros serviços declarados

// --- SERVIÇOS PADRÃO ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 

// --- CONFIGURAÇÃO DO BANCO EM NUVEM REAL (AZURE SQL) ---
builder.Services.AddDbContext<EasyAccessDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    c.RoutePrefix = "swagger-ui.html"; 
});

// Middleware de autorização para validar o pipeline HTTP correto das rotas
app.UseAuthorization(); 

// Comentado para evitar o loop de "Application Error" no contêiner Linux da Azure
// app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();