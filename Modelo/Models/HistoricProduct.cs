﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Modelo.Models
{
    public partial class HistoricProduct
    {
        public int Id { get; set; }
        public int OriginalId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Storage { get; set; }
        public int Ram { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int InStock { get; set; }
        public bool IsActive { get; set; }
        public string RegistrationDate { get; set; }
    }
}