    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace IsFakeViewModels
    {
        public class HistoryCheckViewModel : IdentityUser
        {
           // public string Id { get; set; }
           // public string UserName { get; set; }
            public IEnumerable StatementFile { get; set; }
            public IEnumerable VoiceFile { get; set; }

        }
    }
