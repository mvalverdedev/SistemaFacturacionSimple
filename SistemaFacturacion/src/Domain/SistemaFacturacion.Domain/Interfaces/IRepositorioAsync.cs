using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaFacturacion.Domain.Interfaces
{
    public interface IRepositorioAsync<T> where T : class
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<IReadOnlyList<T>> ObtenerTodosAsync();
        Task<T> AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(T entidad);
    }
}
