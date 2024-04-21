using IsFakeModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public class DetailsOfUserViewModel : IdentityUser 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastLogIn { get; set; } = DateTime.Now;
        public bool IsActivate { get; set; }

        public DetailsOfUserViewModel() { }

        
        public DetailsOfUserViewModel(ApplicationUser model)
        {
            Id         = model.Id;
            UserName   = model.UserName;
            Email      = model.Email;
            CreatedAt  = model.CreatedAt;
            LastLogIn  = model.LastLogIn;
            IsActivate = model.IsActivate;
        }

        public ApplicationUser ConvertViewModel(DetailsOfUserViewModel model)
        {
            return new ApplicationUser
            {
                Id         = model.Id,
                UserName   = model.UserName,
                Email      = model.Email,
                CreatedAt  = model.CreatedAt,
                LastLogIn  = model.LastLogIn,
                IsActivate = model.IsActivate,
            };

        }

    }
}
