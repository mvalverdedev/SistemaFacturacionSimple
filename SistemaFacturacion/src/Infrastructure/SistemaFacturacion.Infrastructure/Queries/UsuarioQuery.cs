using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Application.DTOs;
using SistemaFacturacion.Application.Features.Usuarios.Queries.ObtenerUsuariosPaginados;
using SistemaFacturacion.Application.Interfaces.Queries;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaFacturacion.Infrastructure.Queries
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly ContextoAplicacion _contexto;
        private readonly IMapper _mapper;

        public UsuarioQuery(ContextoAplicacion contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<UsuarioDto>> ObtenerUsuariosPaginadosAsync(ObtenerUsuariosPaginadosQuery request)
        {
            var query = _contexto.Set<Usuario>().AsNoTracking();

            query = AplicarFiltros(query, request);

            if (request.TraerTodo)
            {
                return await query.ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            return await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<UsuarioDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<int> ObtenerTotalRegistrosAsync(ObtenerUsuariosPaginadosQuery request)
        {
            var query = _contexto.Set<Usuario>().AsNoTracking();
            query = AplicarFiltros(query, request);
            return await query.CountAsync();
        }

        private IQueryable<Usuario> AplicarFiltros(IQueryable<Usuario> query, ObtenerUsuariosPaginadosQuery request)
        {
            if (!string.IsNullOrEmpty(request.NombreUsuario))
            {
                query = query.Where(x => x.NombreUsuario.Contains(request.NombreUsuario));
            }

            if (!string.IsNullOrEmpty(request.NombreCompleto))
            {
                query = query.Where(x => x.NombreCompleto.Contains(request.NombreCompleto));
            }

            if (!string.IsNullOrEmpty(request.Rol))
            {
                query = query.Where(x => x.Rol.Contains(request.Rol));
            }

            return query;
        }
    }
}
