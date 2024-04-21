using IsFakeModels;
using IsFakeRepository.Implementation;
using IsFakeRepository.Interface;
using IsFakeViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsFakeServices
{
    public  class EditUser : IEditUser
    {
        /* private readonly IUnitOfWork _unitofwork;
         private readonly UserManager<ApplicationUser> userManager;

         public EditUser(IUnitOfWork unitofwork,
             UserManager<ApplicationUser> _userManager)
         {
             _unitofwork = unitofwork;
             userManager = _userManager;
         }

         public void UpdateUserData(EditProfileViewModel User)
         {
             var model = new EditProfileViewModel().ConvertViewModel(User);

             var modelById = _unitofwork.GenericRepository<ApplicationUser>().GetById(model.Id);
        //     modelById.Id = model.Id;
             modelById.UserName = model.UserName;
             modelById.PasswordHash = model.PasswordHash;

             _unitofwork.GenericRepository<ApplicationUser>().update(modelById);
             _unitofwork.Save();
         }
        */
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitofwork;

        public EditUser(UserManager<ApplicationUser> userManager , IUnitOfWork unitofwork)
        {
            _userManager = userManager;
            _unitofwork = unitofwork;
        }

        public async Task UpdateUserData(EditProfileViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                // Handle user not found
                return;
            }

            // Update user properties
            user.UserName = model.UserName;
            user.PasswordHash = model.Password;
            user.ConfirmPassword = model.ConfirmPassword;
            
            // Save changes to the database
            await _userManager.UpdateAsync(user);
             _unitofwork.Save();

        }
    }
}
