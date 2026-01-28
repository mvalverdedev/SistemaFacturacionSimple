using AutoMapper;
using MediatR;
using SistemaFacturacion.Application.Wrappers;
using SistemaFacturacion.Domain.Interfaces;
using SistemaFacturacion.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Productos.Commands.ActualizarProducto
{
    public class ActualizarProductoHandler : IRequestHandler<ActualizarProductoCommand, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActualizarProductoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Respuesta<int>> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
        {
            var repositorio = _unitOfWork.Repository<Producto>();
            var registro = await repositorio.ObtenerPorIdAsync(request.Id);

            if (registro == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            registro.Nombre = request.Nombre;
            registro.PrecioUnitario = request.PrecioUnitario;
            registro.Stock = request.Stock;
            registro.Activo = request.Activo;

            await repositorio.ActualizarAsync(registro);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new Respuesta<int>(registro.Id);
        }
    }
}
