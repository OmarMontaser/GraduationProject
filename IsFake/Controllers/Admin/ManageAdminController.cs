using IsFakeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.Admin
{
    [Authorize]
    public class ManageAdminController : Controller
    {
        private readonly IManageAdmin _manageadmin;
        public ManageAdminController(IManageAdmin manageadmin)
        {
            _manageadmin = manageadmin;
        }
        
        public async Task<IActionResult> GetAllAdmins()
        {
            var adminViewModels = await _manageadmin.GetAll(); // Await the async method here

            return View(adminViewModels);//_manageadmin.GetAll());
        }
        public async Task<IActionResult> GetAllUsers()
        {
            var userViewModels = await _manageadmin.GetAllUsers(); // Await the async method here

            return View(userViewModels);//_manageadmin.GetAll());

        }

        public IActionResult GetDetailsOfUser(string id)
        {
             return View(_manageadmin.GetAdminData(id));
        }

        
    }
}
