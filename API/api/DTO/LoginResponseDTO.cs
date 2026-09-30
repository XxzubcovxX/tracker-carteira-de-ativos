namespace trecker.API.api.DTO
{
    public class LoginResponseDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string UserName { get; set; }
        public string? Token { get; set; }
    }
}
