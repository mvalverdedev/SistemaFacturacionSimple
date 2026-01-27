using System;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositorioAsync<T> Repository<T>() where T : class;
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
