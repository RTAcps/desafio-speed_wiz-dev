namespace Biblioteca.Services.Auth.Jwt
{
    public class JwtAuthModelo
    {
        public string TokenAcesso { get; set; }
        public string TokenType { get; set; }
        public int ExpiraEm { get; set; }
    }
}
