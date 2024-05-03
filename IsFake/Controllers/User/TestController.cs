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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _WHE;
        private readonly string _VoiceStatementPath;
        public TestController(ApplicationDbContext context,
                              UserStatement userStatement,
                              UserManager<ApplicationUser> userManager,
                              IWebHostEnvironment WHE
                               )
        {
            _context = context;
            _userStatement = userStatement;
            _userManager = userManager;
            _WHE = WHE;
            _VoiceStatementPath = $"{_WHE.WebRootPath}/Voices/UserStatementVoices";
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

        /*
        [HttpPost]
        public async Task<IActionResult> UploadVoices(List<IFormFile> files)
        {
            var size = files.Sum(f => f.Length);
            var filePaths = new List<string>();

            ApplicationUser currentUser = await _userManager.GetUserAsync(User); 

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), formFile.FileName);
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }

                    // Create and save UserStatement object inside the loop
                    UserStatement userStatement = new UserStatement
                    {
                        VoiceFile = filePath,
                        CreatedDate = DateTime.Now,
             
                    };
                    _context.UserStatements.Add(userStatement);
                    _context.SaveChanges();
                }
            }

            // Perform comparison with other records using your AI model
            // Once comparison is done, take necessary actions based on the results

            return RedirectToAction("UploadVoices", "Test"); // Redirect to the same page
            //return Ok(new {files.Count, size, filePaths});
        }
        */

        [HttpPost]
        public async Task<IActionResult> UploadVoices(TestProgramViewModel model)
        {
            var userstatement = $"{Guid.NewGuid()}{Path.GetExtension(model.VoiceFile.FileName)}";
            var path = Path.Combine(_VoiceStatementPath, userstatement);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                // Copy file to stream
                await model.VoiceFile.CopyToAsync(stream);
            }

            ApplicationUser currentUser = await _userManager.GetUserAsync(User);

            // Create and save UserStatement object inside the loop
            UserStatement userStatement = new UserStatement
            {
                VoiceFile = path,
                CreatedDate = DateTime.Now,
                ApplicationUser = currentUser,
            };
            _context.UserStatements.Add(userStatement);
            _context.SaveChanges();

            // Perform comparison with other records using your AI model
            // Once comparison is done, take necessary actions based on the results
            return RedirectToAction("UploadVoices", "Test"); // Redirect to the same page

        }

    }
}

