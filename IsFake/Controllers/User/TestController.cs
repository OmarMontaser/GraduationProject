using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IsFake.Controllers.User
{
    [Authorize]
    public class TestController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly CheckVoice _checkvoice;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _WHE;
        private readonly string _VoiceStatementPath;
        private readonly HttpClient _httpClient;

        public TestController(ApplicationDbContext context,
                              CheckVoice checkvoice,
                              UserManager<ApplicationUser> userManager,
                              IWebHostEnvironment WHE,
                              HttpClient httpClient)
        {
            _context = context;
            _checkvoice = checkvoice;
            _userManager = userManager;
            _WHE = WHE;
            _VoiceStatementPath = $"{_WHE.WebRootPath}/Voices/CheckVoices";
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult CheckVoice()
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
        public async Task<IActionResult> CheckVoice(TestProgramViewModel model)
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

                CheckVoice CheckVoiceObj = new CheckVoice
                {
                    VoiceFile = path,
                    CreatedDate = DateTime.Now,
                    ApplicationUser = currentUser,
                };

                _context.CheckVoice.Add(CheckVoiceObj);
                _context.SaveChanges();

                // Call the Flask API
                var flaskResponse = await CallFlaskAPI(path);
                if (flaskResponse != null)
                {
                    ViewBag.Prediction = flaskResponse;
                }
            }

            // Redirect to the same page
            return View("CheckVoice", model);
        }

        private async Task<string> CallFlaskAPI(string filePath)
        {
            using (var form = new MultipartFormDataContent())
            {
                var fileContent = new StreamContent(System.IO.File.OpenRead(filePath));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
                form.Add(fileContent, "file", Path.GetFileName(filePath));

                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5000/predict", form);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}




