using AutoMapper;
using MediatR;
using SistemaFacturacion.Application.Wrappers;
using SistemaFacturacion.Domain.Entities;
using SistemaFacturacion.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaFacturacion.Application.Features.Clientes.Commands.CrearCliente
{
    public class CrearClienteHandler : IRequestHandler<CrearClienteComando, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrearClienteHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Respuesta<int>> Handle(CrearClienteComando request, CancellationToken cancellationToken)
        {
            var nuevoCliente = _mapper.Map<Cliente>(request);
            
            var clienteRepositorio = _unitOfWork.Repository<Cliente>();
            var data = await clienteRepositorio.AgregarAsync(nuevoCliente);
            
            await _unitOfWork.SaveAsync(cancellationToken);
            
            return new Respuesta<int>(data.Id, "Cliente creado exitosamente");
        }
    }
}
