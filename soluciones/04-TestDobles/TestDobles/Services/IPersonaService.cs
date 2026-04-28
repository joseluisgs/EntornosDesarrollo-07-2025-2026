namespace TestDobles.Services;

using TestDobles.Models;

public interface IPersonaService
{
    IEnumerable<Persona> GetAll();
    Persona GetById(int id);
    Persona? FindByEmail(string email);
    Persona Create(Persona persona);
    Persona Update(int id, Persona persona);
    Persona Delete(int id, bool logical = true);
}