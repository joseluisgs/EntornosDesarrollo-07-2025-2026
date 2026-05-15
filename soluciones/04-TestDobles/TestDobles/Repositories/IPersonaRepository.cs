using TestDobles.Models;

namespace TestDobles.Repositories;

public interface IPersonaRepository : ICrudRepository<int, Persona> {
    /// <summary>
    ///     Busca una persona por su email.
    /// </summary>
    /// <param name="email">Email a buscar.</param>
    /// <returns>La persona con ese email o null si no existe.</returns>
    Persona? FindByEmail(string email);
}