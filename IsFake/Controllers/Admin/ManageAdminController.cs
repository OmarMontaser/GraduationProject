using IsFakeServices;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.Admin
{
    public class ManageAdminController : Controller
    {
        private readonly IManageAdmin _manageadmin;
        public ManageAdminController(IManageAdmin manageadmin)
        {
            _manageadmin = manageadmin;
        }
        
        public IActionResult GetAllAdmins()
        {
            return View(_manageadmin.GetAll());
        }
        public async Task<IActionResult> GetAdminById(string id)
        {
            return View(await _manageadmin.GetAdminById(id));
        }


    }
}
