namespace TestDobles.Validators;

using TestDobles.Models;

public interface IValidator<in TEntity> where TEntity : class
{
    /// <summary>
    /// Valida una entidad y devuelve una lista de errores.
    /// </summary>
    /// <param name="entity">Entidad a validar.</param>
    /// <returns>Lista de errores de validación (vacía si es válida).</returns>
    List<string> Validar(TEntity entity);
}