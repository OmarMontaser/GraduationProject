using IsFakeRepository.Interface;
using IsFakeViewModels;
using IsFakeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeServices
{
  public class StatementService : IStatements
  {
     private readonly IUnitOfWork _unitOfWork;
      public StatementService(IUnitOfWork unitOfWork)
      {
            _unitOfWork = unitOfWork;
      }
      public void DeleteStatement(int id)
      {
            var model = _unitOfWork.GenericRepository<Statement>().GetById(id);
            _unitOfWork.GenericRepository<Statement>().Delete(model);
            _unitOfWork.Save();
      }

        public IEnumerable<StatementViewModel> GetAll()
        {

            var modelList = _unitOfWork.GenericRepository<Statement>().GetAll().ToList();
            return ConvertModeltoViewModelList(modelList);
        }

        public StatementViewModel GetStatementById(int StatementId)
        {
            var model = _unitOfWork.GenericRepository<Statement>().GetById(StatementId);
            var vm = new StatementViewModel(model);
            return vm;
        }

        public void InsertStatement(StatementViewModel Statement)
        {
            var model = new StatementViewModel().ConvertViewModel(Statement);
            _unitOfWork.GenericRepository<Statement>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateStatement(StatementViewModel statement)
        {
            var model = new StatementViewModel().ConvertViewModel(statement);

            var modelById = _unitOfWork.GenericRepository<Statement>().GetById(model.StatementId);


            modelById.Text = statement.Text;
         
            _unitOfWork.GenericRepository<Statement>().update(modelById);
            _unitOfWork.Save();
        }
        
        private List<StatementViewModel> ConvertModeltoViewModelList(IEnumerable<Statement> modelList)
        {
            return modelList.Select(x => new StatementViewModel(x)).ToList();
        }

    }
}