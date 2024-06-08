using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public class ChangePasswordViewModel : IdentityUser
    {

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string OldPassword { get; set; } = string.Empty;

       
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
