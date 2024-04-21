using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class Statement
    {
        [Key]
        public int StatementId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /*
        public int UserRecordId { get; set; }
        [ForeignKey("UserRecordId")]
        public UserRecord UserRecord { get; set; }

        public int UserStatementId { get; set; }
        [ForeignKey("UserStatementId")]
        public UserStatement UserStatement { get; set; }
       */

    }
}
