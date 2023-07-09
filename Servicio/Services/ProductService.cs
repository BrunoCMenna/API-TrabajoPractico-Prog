using AutoMapper;
using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.IServices;
using Servicio.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Services
{
    public class ProductService : IProductService
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public ProductService(EcommerceContext _context)
        {
            this._context = _context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<ProductDTO> GetProducts()
        {
            return _mapper.Map<List<ProductDTO>>(_context.Product.ToList());
        }

        public ProductDTO GetProductById(int id)
        {
            var product = _context.Product.Where(w => w.Id == id).FirstOrDefault();
            if (product == null)
            {
                throw new Exception($"Product with ID {id} doesn't exist");
            }
            return _mapper.Map<ProductDTO>(product);
        }

        public ProductDTO AddNewProduct(ProductViewModel product)
        {
            _context.Product.Add(new Product()
            {
                Brand = product.Brand,
                Model = product.Model,
                Storage = product.Storage,
                Ram = product.Ram,
                Description = product.Description,
                Price = product.Price,
          
            });
            _context.SaveChanges();

            var lastProduct = _context.Product.OrderBy(x => x.Id).Last();
            return _mapper.Map<ProductDTO>(lastProduct);
        }

        public ProductDTO UpdateProduct(int id, ProductViewModel product)
        {
            var existingProduct = _context.Product.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {id} doesn't exist");
            }

            existingProduct.Brand = product.Brand;
            existingProduct.Model = product.Model;
            existingProduct.Storage = product.Storage;
            existingProduct.Ram = product.Ram;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            _context.SaveChanges();

            return _mapper.Map<ProductDTO>(_context.Product.Where(w => w.Id == id).First());
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new Exception($"Product with ID {id} doesn't exist");
            } else
            {
                _context.Product.Remove(_context.Product.Single(s => s.Id == id));
                _context.SaveChanges();
            }
            
        }
    }
}
