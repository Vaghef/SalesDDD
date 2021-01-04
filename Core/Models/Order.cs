using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class Order : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool Confirm { get; set; }
    }
}
