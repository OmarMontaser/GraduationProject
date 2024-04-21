using IsFakeModels;
using IsFakeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeServices
{
    public interface IStatements
    {
        IEnumerable<StatementViewModel> GetAll();
        StatementViewModel GetStatementById(int StatementId);
        void UpdateStatement(StatementViewModel Statement);
        void InsertStatement(StatementViewModel Statement);
        void DeleteStatement(int id);

    }
}
