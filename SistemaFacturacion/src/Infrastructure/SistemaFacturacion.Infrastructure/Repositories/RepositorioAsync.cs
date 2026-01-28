using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Domain.Interfaces;
using SistemaFacturacion.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaFacturacion.Infrastructure.Repositories
{
    public class RepositorioAsync<T> : IRepositorioAsync<T> where T : class
    {
        private readonly ContextoAplicacion _dbContext;

        public RepositorioAsync(ContextoAplicacion dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> ObtenerPorIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ObtenerTodosAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ObtenerAsync(System.Linq.Expressions.Expression<System.Func<T, bool>> predicado)
        {
            return await _dbContext.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task<T> AgregarAsync(T entidad)
        {
            await _dbContext.Set<T>().AddAsync(entidad);
            return entidad;
        }

        public Task ActualizarAsync(T entidad)
        {
            _dbContext.Entry(entidad).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task EliminarAsync(T entidad)
        {
            _dbContext.Set<T>().Remove(entidad);
            return Task.CompletedTask;
        }
    }
}
