using IsFakeModels;
using IsFakeRepository.Interface;
using IsFakeServices;
using IsFakeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.Admin
{

    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> AdminProfile()
        {
            // Get the currently logged-in user ID
            var userId = _userManager.GetUserId(User);

            // Fetch the user profile
            var profile = await GetUserProfileAsync(userId);

            // Check if the profile is null (user not found)
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        private async Task<ProfileViewModel> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var profile = new ProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
               
            };

            return profile;
        }
    }

}
