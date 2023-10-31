using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTO
{
    public class TopProductsDTO
    {
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Storage { get; set; }
        public int Ram { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool isActive { get; set; }
        public int inStock { get; set; }
        public int TotalQuantity { get; set; }
        public string Image { get; set; }
    }
}
