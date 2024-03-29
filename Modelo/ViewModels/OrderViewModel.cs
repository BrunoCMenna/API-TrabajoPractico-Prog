﻿using Modelo.DTO;
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
        //de aca para abajo es nuevo
        public string Phone { get; set; }
        public string Province { get; set; }
        public string NameLastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
