using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTO;
using Modelo.ViewModels;
using Servicio.IServices;
using System.Security.Claims;

namespace API_TrabajoPractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublicUserController : ControllerBase
    {
        private readonly IPublicUserService _service;

        public PublicUserController(IPublicUserService service)
        {
            _service = service;
        }

        [HttpPut("ChangePassword")]
        public ActionResult<string> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var response = _service.ChangePassword(model.CurrentPassword, model.NewPassword, userId);

                if (response == null)
                {
                    return NotFound("User not found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("ChangeInfo")]
        public ActionResult<UserDTO> ChangeInfo([FromBody] ChangeInfoViewModel model)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var response = _service.ChangeInfo(model, userId);

                if (response == null)
                {
                    return NotFound("User not found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
