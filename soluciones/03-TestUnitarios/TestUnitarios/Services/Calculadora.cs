namespace TestUnitarios.Services;

public class Calculadora
{
    public int Sumar(int a, int b) => a + b;
    public int Restar(int a, int b) => a - b;
    public decimal Dividir(decimal a, decimal b)
    {
        if (b == 0) throw new DivideByZeroException("No se puede dividir por cero");
        if (a < 0) throw new ArgumentException("El dividendo no puede ser negativo");
        return a / b;
    }
    public int Multiplicar(int a, int b) => a * b;
}