using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicio.IServices;
using Modelo.ViewModels;

namespace API_TrabajoPractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] LoginViewModel User)
        {
            string response = string.Empty;
            try
            {
                response = _service.Login(User);
                if (string.IsNullOrEmpty(response))
                    return NotFound("Incorrect email/password");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }

        [HttpPost("SignIn")]
        public ActionResult<string> SignIn([FromBody] SignInViewModel User)
        {
            string response = string.Empty;
            try
            {
                response = _service.SignIn(User);
                if (response == "Email is required" || response == "Password is required" || response == "Email is already in use")
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }
    }
}
