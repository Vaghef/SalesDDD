using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Product : BaseEntity
    {
        public Unit Unit { get; set; }
        public int UnitId { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        public string ProductCode { get; set; }
        public string Size { get; set; }
        public int Qty { get; set; }
        public byte[] ProductImage { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
    }
}
