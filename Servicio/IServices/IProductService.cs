using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.IServices
{
    public interface IProductService
    {
        List<ProductDTO> GetProducts();
        ProductDTO GetProductById(int id);
        ProductDTO AddNewProduct(ProductViewModel product);
        ProductDTO UpdateProduct(int id, ProductViewModel product);
        string DeleteProduct(int id);
    }
}
