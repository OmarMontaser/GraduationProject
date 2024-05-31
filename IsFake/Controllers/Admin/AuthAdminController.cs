using IsFakeModels;
using IsFakeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.Admin
{
//    [Authorize(Roles ="Admin")]
    public class AuthAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthAdminController(UserManager<ApplicationUser> _UserManager,
           SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _UserManager;
            signInManager = _signInManager;
        }
       // [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //[Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
       // [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterAdminViewModel newUser)
        {
            ApplicationUser userModel = new ApplicationUser();
            userModel.FirstName = newUser.FirstName;
            userModel.LastName = newUser.LastName;
            userModel.UserName = newUser.UserName;
            userModel.Email = newUser.Email;
            userModel.PasswordHash = newUser.Password;
            userModel.ConfirmPassword = newUser.ConfirmPassword;


            IdentityResult result = await userManager.CreateAsync(userModel, newUser.Password);

            if (result.Succeeded == true)
            {
                //Assign Role 
                await userManager.AddToRoleAsync(userModel, "Admin");
               
                //Create Cookie;
                await signInManager.SignInAsync(userModel, false);
                return RedirectToAction("Index" , "AuthAdmin");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(newUser);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }


        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInAdminViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await userManager.FindByNameAsync(uservm.UserName);
                if (userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, uservm.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, uservm.RememberMe);
                        //return RedirectToAction("Index");
                        return RedirectToAction("Index", "AuthAdmin");
                    }
                }
                ModelState.AddModelError("", "Username & password is wrong");
            }
            return View(uservm);
        }
    }
}
