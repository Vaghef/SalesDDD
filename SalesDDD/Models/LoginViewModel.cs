using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesDDD.Models
{
    public class LoginViewModel
    {
        [Display(Name = nameof(UserName), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string UserName { get; set; }

        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    public class ChangePasswordViewModel
    {

        [DataType(DataType.Password)]
        [Display(Name = nameof(OldPassword), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(40, ErrorMessage = "طول رمز عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        [Display(Name = nameof(Password), ResourceType = typeof(Resources.Labels))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Resources.Labels))]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن برابر نیستند")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resources.Messages))]
        public string ConfirmPassword { get; set; }

    }

    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = nameof(Resources.Messages.RequiredField))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword), ResourceType = typeof(Resources.Labels))]

        [StringLength(40, ErrorMessage = "طول رمز عبور باید حداقل 6 حرف باشد", MinimumLength = 6)]
        public string NewPassword { get; set; }

        public string Code { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Messages), ErrorMessageResourceName = nameof(Resources.Messages.RequiredField))]
        [Display(Name = nameof(Mobile), ResourceType = typeof(Resources.Labels))]

        public string Mobile { get; set; }
    }
}
