using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Models
{
    public enum RoleType
    {
        [Display(Name = "مدیر")]
        Admin,
        [Display(Name = "اپراتور")]
        Operator,
        [Display(Name = "مشتری")]
        Customer
    }
}
