using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Models
{
    public class OrderViewModel : BaseViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = nameof(ProductTitle), ResourceType = typeof(Resources.Labels))]
        public string ProductTitle { get; set; }
        public Product Product { get; set; }
        public bool Confirm { get; set; }
        [Display(Name = nameof(Profit), ResourceType = typeof(Resources.Labels))]
        public double Profit { get; set; }
    }
}
