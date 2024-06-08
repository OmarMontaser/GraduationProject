using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IsFake.Controllers.User
{

    [Authorize]
    public class HistoryCompareController : Controller
    {        
            public readonly ApplicationDbContext _context;
            public readonly UserStatement _userStatement;
            public readonly UserRecord _userRecord;
            public readonly UserManager<ApplicationUser> _userManager;
            public readonly CheckVoice _checkvoice;
            // public readonly IdentityUser _identityUser;
            public HistoryCompareController(ApplicationDbContext context,
                                  UserStatement userStatement,
                                  UserRecord userRecord,
                                  UserManager<ApplicationUser> userManager,
                                  CheckVoice checkVoice
                                    // IdentityUser identityUser
                                    )
            {
                _context = context;
                _userStatement = userStatement;
                _userRecord = userRecord;
                _userManager = userManager;
                _checkvoice = checkVoice;
                // _identityUser = identityUser;
            }


            [HttpGet]
            public async Task<IActionResult> History()
            {
                ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound();
                }

              
                var uuserStatements = _context.UserStatements
                    .Where(us => us.ApplicationUserId == currentUser.Id)
                    .Select(us => us.VoiceFile) // Select the specific property you want to display
                    .ToList();

                var record = _context.UserRecords
                .Where(us => us.ApplicationUserId == currentUser.Id)
                .Select(us => us.RecordFile) // Select the specific property you want to display
                .ToList();


            // Construct a view model to pass data to the view
            var viewModel = new List<HistoryCompareViewModel>
            {
                    new HistoryCompareViewModel
            {
                StatementFile = uuserStatements.ToList(),
                VoiceFile = record.ToList()

            }
            };     

                return View(viewModel);
            }

    }
}
