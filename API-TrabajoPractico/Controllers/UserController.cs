using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.IServices;

namespace API_TrabajoPractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "RequireAdminRole")]
    public class UserController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly IUserService _service;

        public UserController(EcommerceContext context ,IUserService userService)
        {
            _context = context;
            _service = userService;
        }

        [HttpGet("GetUsers")]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                var response = _service.GetUsers();
                if (response.Count == 0)
                {
                    NotFound("There are no registered users");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        public ActionResult<ProductDTO> GetUserById([FromRoute] int id)
        {
            try
            {
                var response = _service.GetUserById(id);
                if (response == null)
                {
                    return NotFound($"User with ID {id} not found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("CreateUserWithRole")]
        public ActionResult<string> CreateUserWithRole([FromBody] UserViewModel User)
        {
            string response = string.Empty;
            try
            {
                response = _service.CreateUserWithRole(User);
                if (response == "Email is required" || response == "Password is required" || response == "Email is already in use" || response == "Role ID doesn't exist")
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }

            return Ok(response);
        }

        [HttpPut("UpdateUserRole/{id}/{roleId}")]
        public ActionResult<UserDTO> UpdateUserRole([FromRoute] int id, [FromRoute] int roleId)
        {
            try
            {
                var response = _service.UpdateUserRole(id, roleId);
                if (response == null)
                {
                    var userExists = _context.User.Any(p => p.Id == id);
                    if (!userExists)
                    {
                        return NotFound($"User with ID {id} not found");
                    }

                    var roleExists = _context.Role.Any(r => r.Id == roleId);
                    if (!roleExists)
                    {
                        return NotFound($"Role with ID {roleId} not found");
                    }
                }

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/User/GetUserById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{id}";
                return Created(locationUrl, response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public ActionResult<string> DeleteUser([FromRoute] int id)
        {
            string response = string.Empty;
            try
            {
                response = _service.DeleteUser(id);
                if (response == null)
                {
                    return NotFound($"User with ID {id} not found");
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
