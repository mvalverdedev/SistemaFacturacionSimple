using AutoMapper;
using MediatR;
using APIFacturacion.Application.Wrappers;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace APIFacturacion.Application.Features.Productos.Commands.CrearProducto
{
    public class CrearProductoHandler : IRequestHandler<CrearProductoCommand, Respuesta<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrearProductoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Respuesta<int>> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
        {
            var nuevoRegistro = _mapper.Map<Producto>(request);
            var repositorio = _unitOfWork.Repository<Producto>();
            var data = await repositorio.AgregarAsync(nuevoRegistro);
            await _unitOfWork.SaveAsync(cancellationToken);
            return new Respuesta<int>(data.Id);
        }
    }
}

