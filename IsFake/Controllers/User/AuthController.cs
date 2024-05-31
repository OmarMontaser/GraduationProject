using IsFakeModels;
using IsFakeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.User
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthController(UserManager<ApplicationUser> _UserManager,
           SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _UserManager;
            signInManager = _signInManager;            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUser)
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
                //Create Cookie;
                await signInManager.SignInAsync(userModel, false);
                return RedirectToAction("Index", "Auth");
            } else
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
        public async Task<IActionResult> LogIn(LogInUserViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await userManager.FindByNameAsync(uservm.UserName);
                if(userModel != null)
                {
                   bool found =await userManager.CheckPasswordAsync(userModel, uservm.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, uservm.RememberMe);
                        userModel.LastLogIn = DateTime.Now;
                        await userManager.UpdateAsync(userModel);
                        return RedirectToAction("Index");
                    }
                }
                ModelState.AddModelError("", "Username & password is wrong");
            }
            return View(uservm);     
        }
    }
}
