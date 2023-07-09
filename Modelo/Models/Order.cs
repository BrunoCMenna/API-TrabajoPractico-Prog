﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}