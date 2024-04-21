using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class UserStatement
    {
        [Key]
        public int UserStatementId { get; set; }
        public string VoiceFile { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now ;


        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

      //  public Statement Statement { get; set; }
    }
}
