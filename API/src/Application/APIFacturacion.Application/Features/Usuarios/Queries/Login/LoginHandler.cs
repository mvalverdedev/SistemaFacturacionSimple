using MediatR;
using APIFacturacion.Application.DTOs;
using APIFacturacion.Application.Interfaces.Services;
using APIFacturacion.Application.Wrappers;
using APIFacturacion.Domain.Entities;
using APIFacturacion.Domain.Interfaces;
using APIFacturacion.Encryption;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace APIFacturacion.Application.Features.Usuarios.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, Respuesta<LoginResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public LoginHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<Respuesta<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Repository<Usuario>().ObtenerAsync(u => u.NombreUsuario == request.NombreUsuario);
            
            if (users.Count == 0)
            {
                throw new KeyNotFoundException($"Usuario {request.NombreUsuario} no encontrado.");
            }

            var usuario = users[0];

            if (!_passwordHasher.Verify(request.Clave, usuario.Clave))
            {
                 throw new UnauthorizedAccessException("Credenciales inválidas."); 
            }
            
            if (!usuario.Activo)
            {
                 throw new UnauthorizedAccessException("El usuario está inactivo.");
            }

            var token = _jwtService.GenerateToken(usuario);

            var response = new LoginResponse
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Rol = usuario.Rol,
                Token = token
            };

            return new Respuesta<LoginResponse>(response);
        }
    }
}

