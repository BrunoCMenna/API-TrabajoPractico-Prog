using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Storage { get; set; }
        public int Ram { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //pueba
        public string Image { get; set; }
    }
}
