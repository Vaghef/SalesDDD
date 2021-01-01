using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Claims = new List<string>();
        }
        public string Id { get; set; }
        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string UserName { get; set; }
        [Display(Name = nameof(Email), ResourceType = typeof(Resources.Labels))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = nameof(Mobile), ResourceType = typeof(Resources.Labels))]
        public string Mobile { get; set; }
        [Display(Name = nameof(PhoneNumber), ResourceType = typeof(Resources.Labels))]
        public string PhoneNumber { get; set; }
        [Display(Name = nameof(FirstName), ResourceType = typeof(Resources.Labels))]
        public string FirstName { get; set; }
        [Display(Name = nameof(LastName), ResourceType = typeof(Resources.Labels))]
        public string LastName { get; set; }
        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string Password { get; set; }
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Resources.Labels))]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string ConfirmPassword { get; set; }
        [Display(Name = nameof(RoleType), ResourceType = typeof(Resources.Labels))]
        public RoleType RoleType { get; set; }
        [Display(Name = "دسترسی ها")]
        public List<string> Claims { get; set; }
    }
}
