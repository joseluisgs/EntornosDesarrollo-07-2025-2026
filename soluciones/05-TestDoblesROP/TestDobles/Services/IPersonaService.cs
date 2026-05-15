namespace TestDobles.Services;

using TestDobles.Models;
using TestDobles.Errors;
using CSharpFunctionalExtensions;

public interface IPersonaService
{
    IEnumerable<Persona> GetAll();
    Result<Persona, PersonaError> GetById(int id);
    Result<Persona, PersonaError> FindByEmail(string email);
    Result<Persona, PersonaError> Create(Persona persona);
    Result<Persona, PersonaError> Update(int id, Persona persona);
    Result<Persona, PersonaError> Delete(int id, bool logical = true);
}