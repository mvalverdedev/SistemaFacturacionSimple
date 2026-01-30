using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Features.Productos.Queries.ObtenerProductosPaginados;
using APIFacturacion.Application.Interfaces.Queries;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFacturacion.Infrastructure.Queries
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

            if (request.SoloConStock.HasValue && request.SoloConStock.Value)
            {
                query = query.Where(x => x.Stock > 0);
            }

            return query;
        }
    }
}

