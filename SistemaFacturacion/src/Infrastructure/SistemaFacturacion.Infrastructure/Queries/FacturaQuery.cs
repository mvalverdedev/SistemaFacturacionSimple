using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Facturas.Queries.ObtenerFacturasPaginadas;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Infrastructure.Queries
{
    public class FacturaQuery : IFacturaQuery
    {
        private readonly ContextoAplicacion _contexto;
        private readonly IMapper _mapper;

        public FacturaQuery(ContextoAplicacion contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<FacturaDto>> ObtenerFacturasPaginadasAsync(ObtenerFacturasPaginadasQuery request)
        {
            var query = _contexto.Set<Factura>().AsNoTracking();

            query = AplicarFiltros(query, request);

            if (request.TraerTodo)
            {
                return await query.ProjectTo<FacturaDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            return await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<FacturaDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<int> ObtenerTotalRegistrosAsync(ObtenerFacturasPaginadasQuery request)
        {
            var query = _contexto.Set<Factura>().AsNoTracking();
            query = AplicarFiltros(query, request);
            return await query.CountAsync();
        }

        public async Task<FacturaDetalleDto> ObtenerFacturaPorIdAsync(int id)
        {
            return await _contexto.Set<Factura>()
                .Include(f => f.Cliente)
                .Include(f => f.Usuario)
                .Include(f => f.Detalles)
                    .ThenInclude(d => d.Producto)
                .AsNoTracking()
                .Where(f => f.Id == id)
                .ProjectTo<FacturaDetalleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        private IQueryable<Factura> AplicarFiltros(IQueryable<Factura> query, ObtenerFacturasPaginadasQuery request)
        {
            if (!string.IsNullOrEmpty(request.NumeroFactura))
            {
                query = query.Where(x => x.NumeroFactura.Contains(request.NumeroFactura));
            }

            if (request.FechaCreacion.HasValue)
            {
                query = query.Where(x => x.FechaCreacion.Date == request.FechaCreacion.Value.Date);
            }

            if (request.Total.HasValue)
            {
                query = query.Where(x => x.Total == request.Total.Value);
            }

            return query;
        }
    }
}
