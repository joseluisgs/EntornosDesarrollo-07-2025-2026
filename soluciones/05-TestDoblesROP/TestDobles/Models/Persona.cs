namespace TestDobles.Models;

public record Persona(int Id, string Nombre, string Email, string Password, string DNI, int Edad, double Calificacion, bool IsDeleted = false)
{
    public string CalificacionTexto => CalcularCalificacion(Calificacion);

    private static string CalcularCalificacion(double nota)
    {
        return nota switch
        {
            < 0 or > 10 => throw new ArgumentException("La calificación debe estar entre 0 y 10"),
            <= 4 => "Suspenso",
            <= 6 => "Aprobado",
            <= 8 => "Notable",
            _ => "Sobresaliente"
        };
    }
}