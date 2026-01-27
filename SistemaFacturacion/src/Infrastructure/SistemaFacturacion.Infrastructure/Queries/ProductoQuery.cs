using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Infrastructure.Queries
{
    public class ProductoQuery : IProductoQuery
    {
        private readonly ContextoAplicacion _contexto;
        private readonly IMapper _mapper;

        public ProductoQuery(ContextoAplicacion contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductoDto>> ObtenerProductosPaginadosAsync(ObtenerProductosPaginadosQuery request)
        {
            var query = _contexto.Set<Producto>().AsNoTracking();

            query = AplicarFiltros(query, request);

            if (request.TraerTodo)
            {
                return await query.ProjectTo<ProductoDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            return await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProductoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<int> ObtenerTotalRegistrosAsync(ObtenerProductosPaginadosQuery request)
        {
            var query = _contexto.Set<Producto>().AsNoTracking();
            query = AplicarFiltros(query, request);
            return await query.CountAsync();
        }

        private IQueryable<Producto> AplicarFiltros(IQueryable<Producto> query, ObtenerProductosPaginadosQuery request)
        {
            if (!string.IsNullOrEmpty(request.Codigo))
            {
                query = query.Where(x => x.Codigo.Contains(request.Codigo));
            }

            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(x => x.Nombre.Contains(request.Nombre));
            }

            return query;
        }
    }
}
