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
    public class ManageAdminService : IManageAdmin
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageAdminService(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public DetailsOfUserViewModel GetAdminData(string id)
        {
            var model = _unitOfWork.GenericRepository<ApplicationUser>().GetById(id);
            var vm = new DetailsOfUserViewModel(model);
            return vm;
        }

        public async Task<IEnumerable<ManageAdminViewModel>> GetAll()
        {
            var allUsers = _unitOfWork.GenericRepository<ApplicationUser>().GetAll();
            var adminRole = await _roleManager.FindByNameAsync("Admin");

            var adminUserIds = _unitOfWork.GenericRepository<IdentityUserRole<string>>()
                .Where(ur => ur.RoleId == adminRole.Id)
                .Select(ur => ur.UserId)
                .ToList();

            var adminUsers = allUsers.Where(u => adminUserIds.Contains(u.Id)).ToList();

            return ConvertModelToViewModelList(adminUsers);
        }

        public async Task<IEnumerable<ManageAdminViewModel>> GetAllUsers()
        {
            var allUsers = _unitOfWork.GenericRepository<ApplicationUser>().GetAll();
            var adminRole = await _roleManager.FindByNameAsync("Admin");

            if (adminRole == null)
            {
                // If the admin role doesn't exist, return an empty list
                return Enumerable.Empty<ManageAdminViewModel>();
            }

            var adminUserIds = _unitOfWork.GenericRepository<IdentityUserRole<string>>()
                .Where(ur => ur.RoleId == adminRole.Id)
                .Select(ur => ur.UserId)
                .ToList();

            // Filter users who are not admins
            var nonAdminUsers = allUsers.Where(u => !adminUserIds.Contains(u.Id)).ToList();

            return ConvertModelToViewModelList(nonAdminUsers);
        }


        private List<ManageAdminViewModel> ConvertModelToViewModelList(IEnumerable<ApplicationUser> modelList)
        {
            return modelList.Select(x => new ManageAdminViewModel(x)).ToList();
        }
    }

}
