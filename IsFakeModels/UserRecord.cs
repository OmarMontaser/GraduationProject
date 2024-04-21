using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class UserRecord
    {
        public int UserRecordId { get; set; }
        public string RecordFile { get; set; }
        public DateTime RecordsDate { get; set; }
        public bool IsActivation { get; set; }

        
    }
}
