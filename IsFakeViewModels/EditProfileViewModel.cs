using IsFakeModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public class EditProfileViewModel : IdentityUser
    {
        //public string Id { get; set; }
        public string? UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }


        public EditProfileViewModel() { }

        public EditProfileViewModel(ApplicationUser model)
        {
            Id = model.Id;
            UserName = model.UserName;
            Password = model.PasswordHash;
            ConfirmPassword = model.ConfirmPassword;

        }

        public ApplicationUser ConvertViewModel(EditProfileViewModel model)
        {
            return new ApplicationUser
            {
                Id = model.Id,
                UserName = model.UserName,
                PasswordHash = model.Password,
                ConfirmPassword = model.ConfirmPassword,
        };

        }
    }
}
