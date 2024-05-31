using IsFakeModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public class ProfileViewModel : ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName {  get; set; }
        public string Email { get; set; }
    }
}
