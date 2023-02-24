using Authentication.API.Data;
using Authentication.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        [HttpGet("status")]
        public string GetStatus()
        {
            return "API funcionando!";
        }

        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate(string login, string senha)
        {
            try
            {
                // recupera o usuário
                var user = await new UserRepository().GetUser(login, senha);

                // verificar se o usuário existe
                if (user == null)
                {
                    return NotFound(new { message = "Credenciais inválidas!" });
                }

                // gera o token 
                var token = JwtTokenService.GenerateToken(user);

                // oculta a senha
                user.Password = string.Empty;

                // retorna os dados
                return Ok(new
                {
                    user,
                    token
                });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("employee")]
        [Authorize(Roles = "admin,employee")]
        public string Employee() => "Funcionário";

        [HttpGet("manager")]
        [Authorize(Roles = "admin")]
        public string Manager() => "Gerente";
    }
}
