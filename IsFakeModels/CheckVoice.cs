using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class CheckVoice
    {
        [Key]
        public int CheckId { get; set; }
        public string VoiceFile { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;



        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
