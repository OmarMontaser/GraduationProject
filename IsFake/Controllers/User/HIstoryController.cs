using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IsFake.Controllers.User
{
    public class HIstoryController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserStatement _userStatement;
        public readonly UserRecord _userRecord;
        public readonly UserManager<ApplicationUser> _userManager;
       // public readonly IdentityUser _identityUser;
        public HIstoryController(ApplicationDbContext context,
                              UserStatement userStatement,
                              UserRecord userRecord,
                              UserManager<ApplicationUser> userManager
                             // IdentityUser identityUser
                                )
        {
            _context = context;
            _userStatement = userStatement;
            _userRecord = userRecord;
            _userManager = userManager;
           // _identityUser = identityUser;
        }


        [HttpGet]
        public async Task<IActionResult> History()
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Handle the case where the current user is not found
                return NotFound();
            }

            // Fetch user records from UserStatement and UserRecord tables
            var userStatements = _context.UserStatements.Where(UserStatement => UserStatement.ApplicationUserId == currentUser.Id);
            var userRecords    = _context.UserRecords.Where(UserRecord => UserRecord.ApplicationUserId == currentUser.Id);

            // Construct a view model to pass data to the view
            var viewModel = new HIstoryViewModel
            {
               // Id = currentUser.Id,
                UserName = currentUser.UserName,
                StatementFile = userStatements.ToList(), // Assuming StatementFile and VoiceFile are lists
                VoiceFile = userRecords.ToList()
            };

            return View(viewModel);
        }


    }
}
