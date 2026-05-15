using TestDobles.Exceptions;
using TestDobles.Models;
using TestDobles.Repositories;
using TestDobles.Validators;

namespace TestDobles.Services;

public class PersonaService(
    IPersonaRepository repository,
    IPersonaValidator validator
) : IPersonaService {
    public IEnumerable<Persona> GetAll() {
        return repository.GetAll();
    }

    public Persona GetById(int id) {
        var persona = repository.GetById(id);
        return persona ?? throw new PersonaException.NotFound(id);
    }

    public Persona? FindByEmail(string email) {
        var persona = repository.FindByEmail(email);
        return persona ?? throw new PersonaException.NotFoundEmail(email);
    }

    public Persona Create(Persona persona) {
        var errores = validator.Validar(persona);
        return errores.Any() ? throw new PersonaException.Validation(errores) : repository.Create(persona);
    }

    public Persona Update(int id, Persona persona) {
        var errores = validator.Validar(persona);
        if (errores.Any())
            throw new PersonaException.Validation(errores);

        var updated = repository.Update(id, persona);
        return updated ?? throw new PersonaException.NotFound(id);
    }

    public Persona Delete(int id, bool logical = true) {
        var deleted = repository.Delete(id, logical);
        return deleted ?? throw new PersonaException.NotFound(id);
    }
}