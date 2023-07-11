using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.IServices;

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

        [HttpPost("CreateOrder")]
        public ActionResult<OrderDTO> CreateOrder([FromBody] OrderViewModel orderDTO)
        {
            try
            {
                var response = _service.CreateOrder(orderDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetOrders")]
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

        [HttpGet("GetOrdersByUserId/{id}")]
        public ActionResult<List<OrderDTO>> GetOrdersByUserId([FromRoute] int id)
        {
            try
            {
                var response = _service.GetOrdersByUserId(id);
                if (response.Count == 0)
                {
                    NotFound($"User ID {id} has no orders.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetOrderById/{id}")]
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

        [HttpPut("UpdateOrderStatus/{id}")]
        public ActionResult<OrderDTO> UpdateOrderStatus([FromRoute] int id ,[FromBody] OrderStatusViewModel status)
        {
            try
            {
                var response = _service.UpdateOrderStatus(id, status);
                if (response == null)
                {
                    NotFound($"Order ID {id} not found.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteOrder/{id}")]
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
