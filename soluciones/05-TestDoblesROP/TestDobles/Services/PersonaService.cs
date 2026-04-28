namespace TestDobles.Services;

using TestDobles.Models;
using TestDobles.Repositories;
using TestDobles.Validators;
using TestDobles.Errors;
using CSharpFunctionalExtensions;

public class PersonaService(IPersonaRepository repository, IPersonaValidator validator) : IPersonaService
{
    public IEnumerable<Persona> GetAll() => repository.GetAll();

    public Result<Persona, PersonaError> GetById(int id)
    {
        var persona = repository.GetById(id);
        if (persona == null)
            return Result.Failure<Persona, PersonaError>(PersonaErrors.NotFound(id));
        
        return Result.Success<Persona, PersonaError>(persona);
    }

    public Result<Persona, PersonaError> FindByEmail(string email)
    {
        var persona = repository.FindByEmail(email);
        if (persona == null)
            return Result.Failure<Persona, PersonaError>(PersonaErrors.NotFoundEmail(email));
        
        return Result.Success<Persona, PersonaError>(persona);
    }

    public Result<Persona, PersonaError> Create(Persona persona)
    {
        var errores = validator.Validar(persona);
        if (errores.Any())
            return Result.Failure<Persona, PersonaError>(PersonaErrors.Validation(errores));
        
        var created = repository.Create(persona);
        return Result.Success<Persona, PersonaError>(created!);
    }

    public Result<Persona, PersonaError> Update(int id, Persona persona)
    {
        var errores = validator.Validar(persona);
        if (errores.Any())
            return Result.Failure<Persona, PersonaError>(PersonaErrors.Validation(errores));
        
        var updated = repository.Update(id, persona);
        if (updated == null)
            return Result.Failure<Persona, PersonaError>(PersonaErrors.NotFound(id));
        
        return Result.Success<Persona, PersonaError>(updated);
    }

    public Result<Persona, PersonaError> Delete(int id, bool logical = true)
    {
        var deleted = repository.Delete(id, logical);
        if (deleted == null)
            return Result.Failure<Persona, PersonaError>(PersonaErrors.NotFound(id));
        
        return Result.Success<Persona, PersonaError>(deleted);
    }
}