using Microsoft.AspNetCore.Mvc;
using trecker.API.api.DTO;
using trecker.API.api.Service;

namespace trecker.API.api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginService _loginService;

        public AuthController()
        {
            _loginService = new LoginService();
            _loginService.CriarUsuarioPadrao(); // Garante o admin padrao no banco
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool valido = _loginService.ValidarLogin(dto.UsuarioNome, dto.Senha);

            if (!valido)
            {
                return Unauthorized(new LoginResponseDTO
                {
                    Sucesso = false,
                    Mensagem = "Usuario e senha incorretos."
                });
            }

            return Ok(new LoginResponseDTO
            {
                Sucesso = true,
                Mensagem = "Login efetuado com sucesso!",
                UserName = dto.UsuarioNome
            });
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool cadastrado = _loginService.CadastrarUsuario(dto.UsuarioNome, dto.Senha);

            if (!cadastrado)
            {
                return BadRequest(new LoginResponseDTO
                {
                    Sucesso = false,
                    Mensagem = "Nome de usuario ja cadastrado no sistema."
                });
            }

            return Ok(new LoginResponseDTO
            {
                Sucesso = true,
                Mensagem = "Usuario cadastrado com sucesso!",
                UserName = dto.UsuarioNome
            });
        }
    }
}