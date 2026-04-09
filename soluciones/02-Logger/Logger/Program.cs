using Logger.Services;

using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

// =====================================================
// Configuración HARDCODEADA de Serilog
// =====================================================
Console.WriteLine("=== Configuración HARDCODEADA de Serilog ===");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(
        outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        restrictedToMinimumLevel: LogEventLevel.Error,
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        fileSizeLimitBytes: 500 * 1024 * 1024,
        rollOnFileSizeLimit: true,
        outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var calculadora = new Calculadora();

try
{
    Console.WriteLine("\n--- Operaciones ---");
    
    calculadora.Sumar(5, 3);
    calculadora.Restar(10, 4);
    calculadora.Multiplicar(6, 7);
    calculadora.Dividir(20, 4);
    calculadora.Potencia(2, 10);
    calculadora.Factorial(5);
    
    Console.WriteLine("\n--- Generando ERROR (división por cero) ---");
    calculadora.Dividir(10, 0);
}
catch (Exception ex)
{
    Log.Error(ex, "Excepción capturada");
}

Log.CloseAndFlush();
Console.WriteLine("\nRevisa la carpeta /logs");

Console.WriteLine("\n=== Configuración desde appsettings.json ===");

// =====================================================
// Configuración desde appsettings.json
// =====================================================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build())
    .CreateLogger();

var calculadoraJson = new Calculadora();

try
{
    Console.WriteLine("\n--- Operaciones desde appsettings.json ---");
    
    calculadoraJson.Sumar(15, 25);
    calculadoraJson.Restar(100, 37);
    calculadoraJson.Multiplicar(8, 12);
    calculadoraJson.Dividir(100, 4);
    calculadoraJson.Potencia(3, 5);
    calculadoraJson.Factorial(10);
    
    Console.WriteLine("\n--- Generando ERROR (potencia negativa) ---");
    calculadoraJson.Potencia(2, -5);
}
catch (Exception ex)
{
    Log.Error(ex, "Excepción capturada desde appsettings.json");
}

Log.CloseAndFlush();
Console.WriteLine("\nRevisa la carpeta /logs");
Console.WriteLine("\n=== Fin del programa ===");
