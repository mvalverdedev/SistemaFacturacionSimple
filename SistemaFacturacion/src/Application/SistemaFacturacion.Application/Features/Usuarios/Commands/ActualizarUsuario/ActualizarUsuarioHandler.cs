using AutoMapper;
using MediatR;
using SistemaFacturacion.Application.Wrappers;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Usuarios.Commands.ActualizarUsuario
{
    public class ActualizarUsuarioHandler : IRequestHandler<ActualizarUsuarioCommand, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SistemaFacturacion.Encryption.IPasswordHasher _passwordHasher;

        public ActualizarUsuarioHandler(IUnitOfWork unitOfWork, IMapper mapper, SistemaFacturacion.Encryption.IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Respuesta<int>> Handle(ActualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var repositorio = _unitOfWork.Repository<Usuario>();
            var registro = await repositorio.ObtenerPorIdAsync(request.Id);

            if (registro == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }

            if (!string.IsNullOrEmpty(request.Clave))
            {
                registro.Clave = _passwordHasher.Hash(request.Clave);
            }
            // Si la clave viene vacía, no se actualiza (se mantiene la anterior)

            registro.NombreCompleto = request.NombreCompleto;
            registro.Rol = request.Rol;
            registro.Activo = request.Activo;

            await repositorio.ActualizarAsync(registro);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new Respuesta<int>(registro.Id);
        }
    }
}
