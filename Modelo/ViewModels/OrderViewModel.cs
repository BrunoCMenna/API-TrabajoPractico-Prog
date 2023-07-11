using Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ViewModels
{
    public class OrderViewModel
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
