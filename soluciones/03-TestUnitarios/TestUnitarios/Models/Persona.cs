namespace TestUnitarios.Models;

public record Persona(string Nombre, string Email, string Password, string Dni, int Edad, double Calificacion)
{
    public string CalificacionTexto => CalcularCalificacion(Calificacion);

    private string CalcularCalificacion(double nota) {
        return nota switch {
            < 0 or > 10 => throw new ArgumentException("La calificación debe estar entre 0 y 10"),
            <= 4 => "Suspenso",
            <= 6 => "Aprobado",
            <= 8 => "Notable",
            _ => "Sobresaliente"
        };
    }
}