using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IsFake.Controllers.User
{
    public class TestController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserStatement _userStatement;
        public readonly UserRecord _userRecord;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _WHE;
        private readonly string _VoiceStatementPath;
        private readonly string _VoiceRecordPath;
        public TestController(ApplicationDbContext context,
                              UserStatement userStatement,
                              UserRecord userRecord,
                              UserManager<ApplicationUser> userManager,
                              IWebHostEnvironment WHE
                               )
        {
            _context = context;
            _userStatement = userStatement;
            _userRecord = userRecord;
            _userManager = userManager;
            _WHE = WHE;
            _VoiceStatementPath = $"{_WHE.WebRootPath}/Voices/UserStatementVoices";
            _VoiceRecordPath = $"{_WHE.WebRootPath}/Voices/TestVoices";

        }

        [HttpGet]
        public IActionResult UploadVoices()
        {
            TestProgramViewModel viewModel = new()
            {
                Statements = _context.Statements
                          .Select(c => new SelectListItem
                          {
                              Value = c.StatementId.ToString(),
                              Text = c.Text
                          })
                          .ToList()
            };
            return View(viewModel);
        }

        
        [HttpPost]
        public async Task<IActionResult> UploadVoices(TestProgramViewModel model)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            if (model.VoiceFile != null)
            {
                var userStatement = $"{Guid.NewGuid()}{Path.GetExtension(model.VoiceFile.FileName)}";
                var path = Path.Combine(_VoiceStatementPath, userStatement);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Copy file to stream
                    await model.VoiceFile.CopyToAsync(stream);
                }
                UserStatement userStatementObj = new UserStatement
                {
                    VoiceFile = path,
                    CreatedDate = DateTime.Now,
                    ApplicationUser = currentUser,
                };

                _context.UserStatements.Add(userStatementObj);
            }
            if (model.RecordFile != null)
            {
                //var recordFile = model.RecordFile;
                var userRecord = $"{Guid.NewGuid()}{Path.GetExtension(model.RecordFile.FileName)}";
                var path2 = Path.Combine(_VoiceRecordPath, userRecord);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    await model.RecordFile.CopyToAsync(stream);
                }
                UserRecord userRecordObj = new UserRecord
                {
                    RecordFile = path2,
                    RecordsDate = DateTime.Now,
                    ApplicationUser = currentUser,
                };
                _context.UserRecords.Add(userRecordObj);
            }

            _context.SaveChanges();

            // Redirect to the same page
            return RedirectToAction("UploadVoices", "Test");
        }
        

    }
}

