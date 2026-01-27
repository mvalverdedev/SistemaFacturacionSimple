using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Clientes.Queries.ObtenerClientesPaginados;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Infrastructure.Queries
{
    public class ClienteQuery : IClienteQuery
    {
        private readonly ContextoAplicacion _contexto;
        private readonly IMapper _mapper;

        public ClienteQuery(ContextoAplicacion contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ClienteDto>> ObtenerClientesPaginadosAsync(ObtenerClientesPaginadosQuery request)
        {
            var query = _contexto.Set<Cliente>().AsNoTracking();

            query = AplicarFiltros(query, request);

            if (request.TraerTodo)
            {
                return await query.ProjectTo<ClienteDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            return await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ClienteDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<int> ObtenerTotalRegistrosAsync(ObtenerClientesPaginadosQuery request)
        {
            var query = _contexto.Set<Cliente>().AsNoTracking();
            query = AplicarFiltros(query, request);
            return await query.CountAsync();
        }

        private IQueryable<Cliente> AplicarFiltros(IQueryable<Cliente> query, ObtenerClientesPaginadosQuery request)
        {
            if (!string.IsNullOrEmpty(request.NombreRazonSocial))
            {
                query = query.Where(x => x.NombreRazonSocial.Contains(request.NombreRazonSocial));
            }

            if (!string.IsNullOrEmpty(request.Identificacion))
            {
                query = query.Where(x => x.Identificacion.Contains(request.Identificacion));
            }

            return query;
        }
    }
}
