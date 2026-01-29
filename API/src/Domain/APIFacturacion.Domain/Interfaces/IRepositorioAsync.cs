using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIFacturacion.Domain.Interfaces
{
    public interface IRepositorioAsync<T> where T : class
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<IReadOnlyList<T>> ObtenerTodosAsync();
        Task<IReadOnlyList<T>> ObtenerAsync(System.Linq.Expressions.Expression<System.Func<T, bool>> predicado);
        Task<T> AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(T entidad);
    }
}

