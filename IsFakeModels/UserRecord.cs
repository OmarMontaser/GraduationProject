using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class UserRecord
    {
        [Key]
        public int UserRecordId { get; set; }
        public string RecordFile { get; set; }
        public DateTime RecordsDate { get; set; } = DateTime.Now ;
        public bool IsActivation { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
       // public Statement Statement { get; set; }

    }
}
