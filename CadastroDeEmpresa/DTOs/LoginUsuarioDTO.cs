namespace CadastroDeEmpresa.DTOs
{
    public class LoginUsuarioDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        // A senha deve ser validada e criptografada antes de ser enviada para o servidor.
        // Isso pode ser feito no serviço que manipula a lógica de negócios.
    }
}
