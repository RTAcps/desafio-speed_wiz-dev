namespace Biblioteca.Services.Auth.Jwt.Interfaces
{
    public interface IJwtAuthGerenciador
    {
        JwtAuthModelo GerarToken(JwtCredenciais credenciais);
    }
}
