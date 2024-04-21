using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using IsFakeModels;

namespace IsFakeViewModels
{
    public class TestProgramViewModel
    {
        [Display(Name = "Statemet")]
        public int StatementId { get;set; }
        public IEnumerable<SelectListItem> Statements { get; set; } = Enumerable.Empty<SelectListItem>();
        
        // from userRecord
        public IFormFile RecordFile { get; set; } = default! ;
        //from userStatement
        public IFormFile VoiceFile { get; set; } = default!;

    }
}
