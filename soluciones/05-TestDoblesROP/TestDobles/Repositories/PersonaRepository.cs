namespace TestDobles.Repositories;

using TestDobles.Models;

public class PersonaRepository : IPersonaRepository
{
    private readonly Dictionary<int, Persona> _personas = [];
    private int _nextId = 1;

    public IEnumerable<Persona> GetAll()
    {
        return _personas.Values;
    }

    public Persona? GetById(int id)
    {
        return _personas.TryGetValue(id, out var persona) ? persona : null;
    }

    public Persona Create(Persona entity)
    {
        var id = _nextId++;
        var persona = entity with { Id = id };
        _personas[id] = persona;
        return persona;
    }

    public Persona? Update(int id, Persona entity)
    {
        if (!_personas.ContainsKey(id))
            return null;
        
        var persona = entity with { Id = id };
        _personas[id] = persona;
        return persona;
    }

    public Persona? Delete(int id, bool logical = true)
    {
        if (!_personas.TryGetValue(id, out var persona))
            return null;

        if (logical)
        {
            var updated = persona with { IsDeleted = true };
            _personas[id] = updated;
            return updated;
        }
        
        _personas.Remove(id);
        return persona;
    }

    public Persona? FindByEmail(string email)
    {
        return _personas.Values.FirstOrDefault(p => p.Email == email);
    }
}