/*using IsFakeModels;
using IsFakeRepository.Interface;
using IsFakeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeServices
{
    public class TestService : ITest
    {

        private readonly IUnitOfWork _unitOfWork;
        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<StatementViewModel> GetAll()
        {

            var modelList = _unitOfWork.GenericRepository<Statement>().GetAll().ToList();
            return ConvertModeltoViewModelList(modelList);
        }

        public void InsertVoice(TestProgramViewModel Statement)
        {
            var model = new TestProgramViewModel().ConvertViewModel(Statement);
            _unitOfWork.GenericRepository<UserStatement>().Add(statement);
            _unitOfWork.Save();
        }


        private List<StatementViewModel> ConvertModeltoViewModelList(IEnumerable<Statement> modelList)
        {
            return modelList.Select(x => new StatementViewModel(x)).ToList();
        }

    }
}
*/