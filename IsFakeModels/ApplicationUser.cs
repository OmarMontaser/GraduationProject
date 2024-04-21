using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
       
        public DateTime CreatedAt { get; set; } = DateTime.Now ;
        public DateTime LastLogIn { get; set; } = DateTime.Now;
        public bool IsActivate { get; set; }


        public ICollection<UserRecord> UserRecords { get; set; }
        public ICollection<UserStatement> UserStatements { get; set; }
    }

} 
