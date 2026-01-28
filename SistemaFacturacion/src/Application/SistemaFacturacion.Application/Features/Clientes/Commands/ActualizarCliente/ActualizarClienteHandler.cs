using AutoMapper;
using MediatR;
using SistemaFacturacion.Application.Wrappers;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Clientes.Commands.ActualizarCliente
{
    public class ActualizarClienteHandler : IRequestHandler<ActualizarClienteCommand, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActualizarClienteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Respuesta<int>> Handle(ActualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var repositorio = _unitOfWork.Repository<Cliente>();
            var registro = await repositorio.ObtenerPorIdAsync(request.Id);

            if (registro == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            registro.NombreRazonSocial = request.NombreRazonSocial;
            registro.Telefono = request.Telefono;
            registro.Correo = request.Correo;
            registro.Direccion = request.Direccion;
            registro.Activo = request.Activo;

            await repositorio.ActualizarAsync(registro);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new Respuesta<int>(registro.Id);
        }
    }
}
