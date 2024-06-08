using IsFakeModels;
using IsFakeRepository.Implementation;
using IsFakeRepository.Interface;
using IsFakeServices;
using IsFakeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.User
{
    public class ChangeUserNameController : Controller
    {
        /*
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEditUser _editUser;

        public ChangeUserNameController(UserManager<ApplicationUser> userManager, IUnitOfWork unitofwork, IEditUser editUser)
        {
            _unitOfWork = unitofwork;
            _userManager = userManager;
            _editUser = editUser;
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUsername()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsername(ChangeUserNameViewModel model)
        {
            var model = _
        }
        */

        /*
                private readonly IEditUser _editUser;
                private readonly IUnitOfWork _unitOfWork;

                public ChangeUserNameController(IEditUser editUser, IUnitOfWork unitofwork)
                {
                    _editUser = editUser;
                    _unitOfWork = unitofwork;
                }

                [HttpGet]
                public IActionResult EditUserName()
                {
                    return View();
                }

                [HttpPost]
                public IActionResult EditUserName(ChangeUserNameViewModel model)
                {
                    _editUser.UpdateUserData(model);
                    return RedirectToAction("EditUserName");
                }

            }
        */
    }
}