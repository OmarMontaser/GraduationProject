/*
using IsFakeRepository.Interface;
using IsFakeServices;
using IsFakeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.Admin
{
    public class EditProfileController : Controller
    {
        private readonly IEditUser _editUser;
        private readonly IUnitOfWork _unitOfWork;

        public EditProfileController(IEditUser editUser, IUnitOfWork unitofwork)
        {
            _editUser = editUser;
            _unitOfWork = unitofwork;
        }

        [HttpGet]
        public IActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel model)
        {
            _editUser.UpdateUserData(model);
            return RedirectToAction("Index");
        }


    }
}*/