using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IsFakeServices;
using IsFakeViewModels;
using IsFakeModels;
using IsFakeRepository.Interface;
using IsFakeRepository.Implementation;

namespace IsFake.Controllers.Admin
{
    [Authorize(Roles="Admin")]
    public class StatementAdminController : Controller
    {
        private readonly IStatements _statement;
        private readonly IUnitOfWork _unitOfWork;
        public StatementAdminController(IStatements statement , IUnitOfWork unitofwork)
        {
            _statement = statement;
            _unitOfWork = unitofwork;
        }
        public IActionResult Index()
        {
            return View(_statement.GetAll());          
        }

        [HttpGet]
        public IActionResult AddStatement()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddStatement(StatementViewModel model)
        {
            _statement.InsertStatement(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewmodel = _statement.GetStatementById(id);
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult Edit(StatementViewModel model)
        {
            _statement.UpdateStatement(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteStatement(int id)
        {
            _statement.DeleteStatement(id);
            return RedirectToAction("Index");
        }

    }
}
