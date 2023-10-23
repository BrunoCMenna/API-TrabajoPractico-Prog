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
using System.Reflection.Emit;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Claims;
using System.Numerics;
using static System.Net.WebRequestMethods;
using static System.Net.Mime.MediaTypeNames;

namespace Servicio.Services
{
    public class OrderService : IOrderService
    {
        private readonly EcommerceContext _context; 
        private readonly IMapper _mapper;

        public OrderService(EcommerceContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public OrderDTO CreateOrder(OrderViewModel orderVM)
        {
            var user = _context.User.Where(w => w.Id == orderVM.UserId).FirstOrDefault();
            if (user == null)
            {
                throw new Exception($"User ID {orderVM.UserId} not found.");
            }

            Order newOrder = new Order
            {
                UserId = orderVM.UserId,
                Phone = orderVM.Phone,
                Province = orderVM.Province,
                ShippingAddress = orderVM.ShippingAddress,
                ZipCode = orderVM.ZipCode,
                OrderTotal = orderVM.OrderTotal,
                OrderDate = DateTime.Now,
                OrderStatus = orderVM.OrderStatus,
                NameLastName = orderVM.NameLastName,
                Email = orderVM.Email,
                City = orderVM.City
            };

            _context.Order.Add(newOrder);
            _context.SaveChanges();

            foreach (var item in orderVM.OrderItems)
            {
                var product = _context.Product.Where(w => w.Id == item.ProductId).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception($"Product ID {item.ProductId} not found.");
                }

                OrderItem orderItem = new OrderItem
                {
                    OrderId = newOrder.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Image = product.Image,
                    Brand = product.Brand,
                    Model = item.Model,
                    UnitaryPrice = product.Price,
                    TotalPrice = product.Price * item.Quantity
                };

                _context.OrderItem.Add(orderItem);
            }

            _context.SaveChanges();

            var orderDto = _context.Order
                            .Where(o => o.Id == newOrder.Id)
                            .Select(o => new OrderDTO
                            {
                                Id = o.Id,
                                UserId = o.UserId,
                                Phone = o.Phone,
                                Province = o.Province,
                                ShippingAddress = o.ShippingAddress,
                                ZipCode = o.ZipCode,
                                OrderTotal = o.OrderTotal,
                                OrderDate = o.OrderDate,
                                OrderStatus = o.OrderStatus,
                                NameLastName = o.NameLastName,
                                Email = o.Email,
                                City = o.City,
                                OrderItems = _context.OrderItem.Where(oi => oi.OrderId == o.Id).ToList()
                            })
                            .FirstOrDefault();

            return orderDto;
        }

        public List<OrderDTO> GetOrders()
        {
            var orderDto = _context.Order.Select(o => new OrderDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                Phone = o.Phone,
                Province = o.Province,
                ShippingAddress = o.ShippingAddress,
                ZipCode = o.ZipCode,
                OrderTotal = o.OrderTotal,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                OrderImage = "https://i.imgur.com/YCnrTPL.jpg",
                NameLastName = o.NameLastName,
                Email = o.Email,
                City = o.City,
                OrderItems = _context.OrderItem.Where(oi => oi.OrderId == o.Id).ToList()

            }).ToList();

            return orderDto;
        }

        public List<OrderDTO> GetOrdersByUserId(int id)
        {
            var user = _context.User.Where(w => w.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception($"User ID {id} not found.");
            }

            var orderDto = _context.Order.Where(w => w.UserId == id).Select(o => new OrderDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                ShippingAddress = o.ShippingAddress,
                ZipCode = o.ZipCode,
                OrderTotal = o.OrderTotal,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                OrderItems = _context.OrderItem.Where(oi => oi.OrderId == o.Id).ToList()
            }).ToList();

            return orderDto;
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _context.Order.Where(w => w.Id == id).FirstOrDefault();
            if (order == null)
            {
                return null;
            }

            var orderDto = _context.Order.Where(w => w.Id == id).Select(o => new OrderDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                ShippingAddress = o.ShippingAddress,
                ZipCode = o.ZipCode,
                OrderTotal = o.OrderTotal,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                OrderItems = _context.OrderItem.Where(oi => oi.OrderId == o.Id).ToList()

            }).FirstOrDefault();

            return orderDto;
        }

        public OrderDTO UpdateOrderStatus(int id, OrderStatusViewModel newStatus)
        {
            var existingOrder = _context.Order.FirstOrDefault(p => p.Id == id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.OrderStatus = newStatus.OrderStatus;
            
            _context.SaveChanges();

            OrderDTO updatedOrder = GetOrderById(id);
            return updatedOrder;
        }

        public string DeleteOrder(int id)
        {
            
            var order = _context.Order.FirstOrDefault(p => p.Id == id);
            if (order == null)
            {
                return null;
            }

            var orderItems = _context.OrderItem.Where(p => p.OrderId == id).ToList();

            if (orderItems.Count > 0)
            {
                _context.OrderItem.RemoveRange(orderItems);
                _context.SaveChanges();
            }

            _context.Order.Remove(_context.Order.Single(s => s.Id == id));      
            _context.SaveChanges();

            return $"Order ID {id} succesfully deleted";
        }
    }
}
