namespace TestDobles.Repositories;

using TestDobles.Models;

public interface ICrudRepository<in TKey, TEntity> where TEntity : class
{
    /// <summary>
    /// Obtiene todos los registros activos.
    /// </summary>
    /// <returns>Enumerable de entidades.</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Obtiene un registro por su clave primaria.
    /// </summary>
    /// <param name="id">Clave primaria.</param>
    /// <returns>La entidad o null si no existe.</returns>
    TEntity? GetById(TKey id);

    /// <summary>
    /// Crea un nuevo registro.
    /// </summary>
    /// <param name="entity">Entidad a crear.</param>
    /// <returns>La entidad creada con su ID asignado.</returns>
    TEntity? Create(TEntity entity);

    /// <summary>
    /// Actualiza un registro existente.
    /// </summary>
    /// <param name="id">Clave primaria.</param>
    /// <param name="entity">Entidad con los nuevos datos.</param>
    /// <returns>La entidad actualizada o null si no existe.</returns>
    TEntity? Update(TKey id, TEntity entity);

    /// <summary>
    /// Elimina un registro.
    /// </summary>
    /// <param name="id">Clave primaria.</param>
    /// <param name="logical">true = borrado lógico (por defecto), false = borrado físico.</param>
    /// <returns>La entidad eliminada o null si no existe.</returns>
    TEntity? Delete(TKey id, bool logical = true);
}

