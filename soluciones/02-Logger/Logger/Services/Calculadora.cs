using Serilog;

namespace Logger.Services;

public class Calculadora {
    private readonly ILogger _logger = Log.ForContext<Calculadora>();

    public int Sumar(int a, int b) {
        _logger.Debug("Iniciando operación Sumar con {A} + {B}", a, b);
        var resultado = a + b;
        _logger.Information("Suma completada: {A} + {B} = {Resultado}", a, b, resultado);
        return resultado;
    }

    public int Restar(int a, int b) {
        _logger.Debug("Iniciando operación Restar con {A} - {B}", a, b);
        var resultado = a - b;
        _logger.Information("Resta completada: {A} - {B} = {Resultado}", a, b, resultado);
        return resultado;
    }

    public int Multiplicar(int a, int b) {
        _logger.Debug("Iniciando operación Multiplicar con {A} * {B}", a, b);
        var resultado = a * b;
        _logger.Information("Multiplicación completada: {A} * {B} = {Resultado}", a, b, resultado);
        return resultado;
    }

    public decimal Dividir(decimal dividendo, decimal divisor) {
        _logger.Debug("Iniciando operación Dividir con {Dividendo} / {Divisor}", dividendo, divisor);

        if (divisor == 0) {
            _logger.Error("Intento de división por cero. Dividendo: {Dividendo}", dividendo);
            throw new DivideByZeroException("No se puede dividir por cero");
        }

        var resultado = dividendo / divisor;
        _logger.Information("División completada: {Dividendo} / {Divisor} = {Resultado}", dividendo, divisor,
            resultado);
        return resultado;
    }

    public int Potencia(int baseNum, int exponente) {
        _logger.Debug("Iniciando operación Potencia con {Base}^{Exponente}", baseNum, exponente);

        if (exponente < 0) {
            _logger.Error("Intento de potencia con exponente negativo. Base: {Base}, Exponente: {Exponente}", baseNum,
                exponente);
            throw new ArgumentException("El exponente no puede ser negativo", nameof(exponente));
        }

        var resultado = (int)Math.Pow(baseNum, exponente);
        _logger.Information("Potencia completada: {Base}^{Exponente} = {Resultado}", baseNum, exponente, resultado);
        return resultado;
    }

    public int Factorial(int numero) {
        _logger.Debug("Iniciando operación Factorial de {Numero}", numero);

        if (numero < 0) {
            _logger.Error("Intento de factorial con número negativo: {Numero}", numero);
            throw new ArgumentException("El factorial no está definido para números negativos", nameof(numero));
        }

        if (numero > 20) _logger.Warning("Factorial de número grande: {Numero}. Posible overflow.", numero);

        var resultado = 1;
        for (var i = 2; i <= numero; i++) resultado *= i;

        _logger.Information("Factorial completado: {Numero}! = {Resultado}", numero, resultado);
        return resultado;
    }
}