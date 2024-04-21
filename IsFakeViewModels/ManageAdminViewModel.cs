using IsFakeModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public  class ManageAdminViewModel : IdentityUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActivate { get; set; }

        public ManageAdminViewModel() { }

        public ManageAdminViewModel(ApplicationUser model)
        {
            Id = model.Id;
            UserName = model.UserName;
            Email = model.Email;
            CreatedAt = model.CreatedAt;
           IsActivate = model.IsActivate;
        }

        public ApplicationUser ConvertViewModel(ManageAdminViewModel model)
        {
            return new ApplicationUser
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = model.CreatedAt,
                IsActivate = model.IsActivate,
            };

        }

    }
}
