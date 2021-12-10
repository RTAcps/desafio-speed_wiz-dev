using Biblioteca.Models;
using Biblioteca.Services.Auth.Jwt;
using System;

namespace Biblioteca.Adaptadores
{
    public static class ModeloAdaptadores
    {
        public static JwtCredenciais ParaJwtCredenciais(this Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException();
            }

            return new JwtCredenciais
            {
                Email = usuario.Email,
                Senha = usuario.Senha
            };

        }
    }
}
