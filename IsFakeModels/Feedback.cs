﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeModels
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string FeedbackSubject { get; set;}
        public string FeedbackContent { get; set; }

       // public List<ApplicationUser> ApplicationUser { get; set; }
    }
}
