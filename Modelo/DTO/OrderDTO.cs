using Modelo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string ShippingAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string OrderImage { get; set; }
        public string NameLastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    }
}
