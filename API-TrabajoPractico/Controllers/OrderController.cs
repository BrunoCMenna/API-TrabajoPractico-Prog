using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.IServices;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace API_TrabajoPractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost("CreateOrder"), Authorize]
        public ActionResult CreateOrder([FromBody] OrderViewModel orderDTO)
        {
            try
            {
                var response = _service.CreateOrder(orderDTO);

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };

                var json = JsonSerializer.Serialize(response, options);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetOrdersByUserId/{id}"), Authorize]
        public ActionResult<List<OrderDTO>> GetOrdersByUserId([FromRoute] int id)
        {
            try
            {
                var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (userId != id)
                {
                    return Forbid();
                }

                var response = _service.GetOrdersByUserId(id);
                if (response.Count == 0)
                {
                    return NotFound($"User ID {id} has no orders.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetOrders"), Authorize(Roles = "admin,sysadmin")]
        public ActionResult<List<OrderDTO>> GetOrders()
        {
            try
            {
                var response = _service.GetOrders();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetOrderById/{id}"), Authorize(Roles = "admin,sysadmin")]
        public ActionResult<OrderDTO> GetOrderById([FromRoute] int id)
        {
            try
            {
                var response = _service.GetOrderById(id);
                if (response == null)
                {
                    return NotFound($"Order ID {id} not found.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("UpdateOrderStatus/{id}"), Authorize(Roles = "admin,sysadmin")]
        public ActionResult UpdateOrderStatus([FromRoute] int id ,[FromBody] OrderStatusViewModel status)
        {
            try
            {
                var response = _service.UpdateOrderStatus(id, status);
                if (response == null)
                {
                    return NotFound($"Order ID {id} not found.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteOrder/{id}"), Authorize(Roles = "admin,sysadmin")]
        public ActionResult<string> DeleteOrder([FromRoute] int id)
        {
            try
            {
                var response = _service.DeleteOrder(id);
                if (response == null)
                {
                    return NotFound($"Order with ID {id} not found");
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
