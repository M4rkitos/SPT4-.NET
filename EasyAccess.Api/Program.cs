using Microsoft.EntityFrameworkCore;
using EasyAccess.Infrastructure.Data;
using EasyAccess.Infrastructure.Repositories;
using EasyAccess.Application.Services;
using EasyAccess.Domain.Repositories;
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

// CORREÇÃO: Adicionando o serviço de autorização para evitar o erro de crash no startup
builder.Services.AddAuthorization();

// --- BANCO DE DADOS (COMENTADO PARA RODAR LOCAL SEM SQL SERVER) ---
// builder.Services.AddDbContext<EasyAccessDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
//    ?? "Server=(localdb)\\mssqllocaldb;Database=EasyAccess;Trusted_Connection=True;MultipleActiveResultSets=true"));

// --- INJEÇÃO DE DEPENDÊNCIA ---
//builder.Services.AddScoped<IVagaRepository, VagaRepository>();
//builder.Services.AddScoped<VagaService>();

// --- HEALTH CHECKS (COMENTADO PARA NÃO CHECAR BANCO INEXISTENTE) ---
builder.Services.AddHealthChecks();
// .AddDbContextCheck<EasyAccessDbContext>(); 

// --- OPENTELEMETRY ---
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation() 
        .AddConsoleExporter());

var app = builder.Build();

// --- MIDDLEWARES ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    c.RoutePrefix = string.Empty; // Define o Swagger como página inicial
});

app.UseHttpsRedirection();

// CORREÇÃO: Comentado para evitar erros de configuração de política de acesso durante o vídeo
// app.UseAuthorization(); 

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();