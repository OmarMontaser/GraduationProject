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
        private readonly IUnitOfWork _unitofwork;
        private readonly UserManager<ApplicationUser> userManager;

        public ManageAdminService(IUnitOfWork unitofwork,
            UserManager<ApplicationUser> _userManager)
            {
              _unitofwork = unitofwork;
              _userManager = userManager;
            }

        public DetailsOfUserViewModel GetAdminData(string id)
        {
            var model = _unitofwork.GenericRepository<ApplicationUser>().GetById(id);
            var vm = new DetailsOfUserViewModel(model);
            return vm;
         
        }

        public IEnumerable<ManageAdminViewModel> GetAll()
        {

            var modelList = _unitofwork.GenericRepository<ApplicationUser>().GetAll();//.OrderBy(user =>user.UserName);
            return ConvertModeltoViewModelList(modelList);
        }

        private List<ManageAdminViewModel> ConvertModeltoViewModelList(IEnumerable<ApplicationUser> modelList)
        {
            return modelList.Select(x => new ManageAdminViewModel(x)).ToList();
        }

    }
}
