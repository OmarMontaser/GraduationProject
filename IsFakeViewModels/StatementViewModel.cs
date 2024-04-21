using IsFakeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeViewModels
{
    public class StatementViewModel
    {
        public int StatementId { get; set; }
        public string Text { get; set; }

        public StatementViewModel() { }

        public StatementViewModel(Statement model)
        {
            StatementId = model.StatementId;
            Text = model.Text;
        }

        public Statement ConvertViewModel(StatementViewModel model)
        {
            return new Statement
            {
            StatementId = model.StatementId,
            Text = model.Text,
            };

        }
    }
}
