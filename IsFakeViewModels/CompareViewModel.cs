using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;
using IsFakeModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IsFakeViewModels
{
    public class CompareViewModel
    {
        [Display(Name = "Statemet")]
        public string StatementId { get; set; }
        public IEnumerable<SelectListItem> Statements { get; set; } = Enumerable.Empty<SelectListItem>();


        //from userStatement
        public IFormFile VoiceFile { get; set; } = default!;


        // from userRecord
        public IFormFile RecordFile { get; set; } = default!;

        public string result { get; set; }
    }
}
