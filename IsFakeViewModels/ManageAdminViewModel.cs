using IsFakeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public  class ManageAdminViewModel //: ApplicationUser
    {
        public ApplicationUser Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActivate { get; set; }

        public ManageAdminViewModel() { }

        public ManageAdminViewModel(ApplicationUser model)
        {

            UserName = model.UserName;
            Email = model.Email;
            CreatedAt = model.CreatedAt;
            IsActivate = model.IsActivate;
        }

        public ApplicationUser ConvertViewModel(ManageAdminViewModel model)
        {
            return new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = model.CreatedAt,
                IsActivate = model.IsActivate,
            };

        }

    }
}
