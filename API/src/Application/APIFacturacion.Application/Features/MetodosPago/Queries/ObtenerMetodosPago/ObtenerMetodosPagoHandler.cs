using AutoMapper;
using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Wrappers;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APIFacturacion.Application.Features.MetodosPago.Queries.ObtenerMetodosPago
{
    public class ObtenerMetodosPagoHandler : IRequestHandler<ObtenerMetodosPagoQuery, Respuesta<IReadOnlyList<MetodoPagoDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerMetodosPagoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Respuesta<IReadOnlyList<MetodoPagoDto>>> Handle(ObtenerMetodosPagoQuery request, CancellationToken cancellationToken)
        {
            var repositorio = _unitOfWork.Repository<MetodoPago>();
            var todos = await repositorio.ObtenerTodosAsync();
            
            var filtrados = todos.Where(x => x.Activo).OrderBy(x => x.Nombre).ToList();
            var dtos = _mapper.Map<IReadOnlyList<MetodoPagoDto>>(filtrados);

            return new Respuesta<IReadOnlyList<MetodoPagoDto>>(dtos);
        }
    }
}

