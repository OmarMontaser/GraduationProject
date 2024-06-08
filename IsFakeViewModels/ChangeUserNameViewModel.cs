using IsFakeModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public class ChangeUserNameViewModel : IdentityUser
    {

        public string Id {  get; set; }
        public string UserName { get; set; }

        public string NewUserName { get; set; }

        public ChangeUserNameViewModel() { }

        public ChangeUserNameViewModel(ApplicationUser model)
        {
            Id = model.Id;
            UserName = model.UserName;

        }

        public ApplicationUser ConvertViewModel(ChangeUserNameViewModel model)
        {
            return new ApplicationUser
            {
                Id = model.Id,
                UserName = model.UserName,

            };

        }

    }
   
}
