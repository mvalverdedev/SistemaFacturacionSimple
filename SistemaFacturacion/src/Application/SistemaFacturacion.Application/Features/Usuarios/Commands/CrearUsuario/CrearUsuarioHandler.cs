using AutoMapper;
using MediatR;
using SistemaFacturacion.Application.Wrappers;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Usuarios.Commands.CrearUsuario
{
    public class CrearUsuarioHandler : IRequestHandler<CrearUsuarioCommand, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SistemaFacturacion.Encryption.IPasswordHasher _passwordHasher;

        public CrearUsuarioHandler(IUnitOfWork unitOfWork, IMapper mapper, SistemaFacturacion.Encryption.IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Respuesta<int>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Usuario>(request);
            nuevoRegistro.Clave = _passwordHasher.Hash(request.Clave);
            var repositorio = _unitOfWork.Repository<Usuario>();
            var data = await repositorio.AgregarAsync(nuevoRegistro);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new Respuesta<int>(data.Id);
        }
    }
}
