namespace TestDobles.Exceptions;

public abstract class PersonaException(string message) : Exception(message)
{
    public sealed class NotFound(int id)
        : PersonaException($"No se ha encontrado ninguna persona con el identificador: {id}");

    public sealed class NotFoundEmail(string email)
        : PersonaException($"No se ha encontrado ninguna persona con el email: {email}");

    public sealed class Validation(IEnumerable<string> errors)
        : PersonaException("Se han detectado errores de validación en la entidad.")
    {
        public IEnumerable<string> Errores { get; init; } = errors;
    }
}