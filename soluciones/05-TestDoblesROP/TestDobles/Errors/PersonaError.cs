namespace TestDobles.Errors;

public abstract record PersonaError(string Message)
{
    public sealed record NotFound(int Id) : PersonaError($"No se ha encontrado ninguna persona con el identificador: {Id}");
    
    public sealed record NotFoundEmail(string Email) : PersonaError($"No se ha encontrado ninguna persona con el email: {Email}");
    
    public sealed record Validation(IEnumerable<string> Errors) : PersonaError("Se han detectado errores de validación en la entidad.");
}

public static class PersonaErrors
{
    public static PersonaError NotFound(int id) => new PersonaError.NotFound(id);
    public static PersonaError NotFoundEmail(string email) => new PersonaError.NotFoundEmail(email);
    public static PersonaError Validation(IEnumerable<string> errors) => new PersonaError.Validation(errors);
}