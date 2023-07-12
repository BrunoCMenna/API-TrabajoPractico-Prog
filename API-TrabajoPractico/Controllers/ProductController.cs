using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("GetProducts")]
        public ActionResult<List<ProductDTO>> GetProducts()
        {
            try
            {
                var response = _service.GetProducts();
                if (response.Count == 0)
                {
                    return NotFound("There are no products on the database.");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetProductById/{id}")]
        public ActionResult<ProductDTO> GetProductById([FromRoute] int id)
        {
            try
            {
                var response = _service.GetProductById(id);
                if (response == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("GetTopProducts")]
        public ActionResult<List<TopProductsDTO>> GetTopProducts()
        {
            try
            {
                var response = _service.GetTopProducts();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("AddNewProduct"), Authorize(Roles = "admin")]
        public ActionResult<ProductDTO> AddNewProduct([FromBody] ProductViewModel product)
        {
            try
            {
                var response = _service.AddNewProduct(product);

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/Product/GetProductById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{response.Id}";
                return Created(locationUrl, response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut("UpdateProduct/{id}"), Authorize(Roles = "admin")]
        public ActionResult<ProductDTO> UpdateProduct(int id, [FromBody] ProductViewModel product)
        {
            try
            {
                var response = _service.UpdateProduct(id, product);
                if (response == null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string apiAndEndpointUrl = $"api/Product/GetProductById";
                string locationUrl = $"{baseUrl}/{apiAndEndpointUrl}/{id}";
                return Created(locationUrl, response);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpDelete("DeleteProduct/{id}"), Authorize(Roles = "admin")]
        public ActionResult<string> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var response = _service.DeleteProduct(id);
                if (response == null)
                {
                    return NotFound($"Product with ID {id} not found");
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
