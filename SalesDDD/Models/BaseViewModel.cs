using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SalesDDD.Resources;

namespace SalesDDD.Models
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public DateTime? AddedDate { get; set; }
        public string SystemUserId { get; set; }        
        [Display(Name = nameof(Title), ResourceType = typeof(Resources.Labels))]
        public string Title { get; set; }
    }

    public class KeyValueViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
