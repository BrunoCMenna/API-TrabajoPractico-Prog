﻿using Modelo.DTO;
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
        List<TopProductsDTO> GetTopProducts();
        List<HistoricProductDTO> GetHistoricProducts();
        ProductDTO AddNewProduct(ProductViewModel product);
        ProductDTO UpdateProduct(int id, ProductViewModel product);
        List<TopProductsDTO> GetAllTopProducts();
        string DeleteProduct(int id);
    }
}
