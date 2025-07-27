namespace CadastroDeEmpresa.DTOs
{
    public class RegisterUserDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        // A senha deve ser validada e criptografada antes de ser salva no banco de dados.
        // Isso pode ser feito no serviço que manipula a lógica de negócios.
    }
}
