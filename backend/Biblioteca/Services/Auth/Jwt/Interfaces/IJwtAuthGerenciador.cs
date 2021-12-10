namespace Biblioteca.Services.Auth.Jwt.Interfaces
{
    interface IJwtAuthGerenciador
    {
        JwtAuthModelo GerarToken(JwtCredenciais credenciais);
    }
}
