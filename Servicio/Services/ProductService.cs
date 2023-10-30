using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

//using System.Net;
//using System.Net.Http;
//using System.IO;
//using System.Text;


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
                return null;
            }
            return _mapper.Map<ProductDTO>(product);
        }

        public List<TopProductsDTO> GetTopProducts()
        {
            var productQuantities = _context.OrderItem
                .GroupBy(oi => oi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(oi => oi.Quantity)
                })
                .ToList();

            var topProducts = productQuantities
                .Join(_context.Product, tp => tp.ProductId, p => p.Id, (tp, p) => new TopProductsDTO
                {
                    ProductId = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Storage = p.Storage,
                    Ram = p.Ram,
                    Description = p.Description,
                    Price = p.Price,
                    TotalQuantity = tp.TotalQuantity
                })
                .OrderByDescending(tp => tp.TotalQuantity)
                .Take(3)
                .ToList();

            return topProducts;
        }

        public List<HistoricProductDTO> GetHistoricProducts()
        {
            var historicProductList = _context.HistoricProduct
            .Select(p => new HistoricProductDTO
            {
                Id = p.Id,
                OriginalId = p.OriginalId,
                Brand = p.Brand,
                Model = p.Model,
                Storage = p.Storage,
                Ram = p.Ram,
                Description = p.Description,
                Price = p.Price,
                Image = p.Image,
                InStock = p.InStock,
                IsActive = p.IsActive,
                RegistrationDate = p.RegistrationDate
            })
            .ToList();

            return historicProductList;
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
                Image = product.Image,
                InStock = product.InStock,
                IsActive = product.IsActive
            });
            
            _context.SaveChanges();
            
            var lastProduct = _context.Product.OrderBy(x => x.Id).Last();

            HistoricProduct backupProduct = lastProduct.Clone();
            _context.HistoricProduct.Add(new HistoricProduct
            {
                OriginalId = backupProduct.OriginalId,
                Brand = backupProduct.Brand,
                Model = backupProduct.Model,
                Storage = backupProduct.Storage,
                Ram = backupProduct.Ram,
                Description = backupProduct.Description,
                Price = backupProduct.Price,
                Image = backupProduct.Image,
                InStock = backupProduct.InStock,
                IsActive = backupProduct.IsActive,
                RegistrationDate = backupProduct.RegistrationDate,
            });

            _context.SaveChanges();

            return _mapper.Map<ProductDTO>(lastProduct);
        }

        public ProductDTO UpdateProduct(int id, ProductViewModel product)
        {
            var existingProduct = _context.Product.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            if (product.Price != existingProduct.Price)
            {
                HistoricProduct backupProduct = existingProduct.Clone();
                _context.HistoricProduct.Add(new HistoricProduct
                {
                    OriginalId = backupProduct.OriginalId,
                    Brand = backupProduct.Brand,
                    Model = backupProduct.Model,
                    Storage = backupProduct.Storage,
                    Ram = backupProduct.Ram,
                    Description = backupProduct.Description,
                    Price = product.Price,
                    Image = backupProduct.Image,
                    InStock = backupProduct.InStock,
                    IsActive = backupProduct.IsActive,
                    RegistrationDate = backupProduct.RegistrationDate,
                });
            }

            existingProduct.Brand = product.Brand;
            existingProduct.Model = product.Model;
            existingProduct.Storage = product.Storage;
            existingProduct.Ram = product.Ram;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.InStock = product.InStock;
            existingProduct.IsActive = product.IsActive;
            _context.SaveChanges();

            return _mapper.Map<ProductDTO>(_context.Product.Where(w => w.Id == id).First());
        }

        public string DeleteProduct(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            } else
            {
                product.IsActive = false;
                _context.SaveChanges();
                return $"Product ID {id} successfully deleted";

            }

            
            

           
        }
    }
}
