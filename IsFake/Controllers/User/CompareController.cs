using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace IsFake.Controllers.User
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    [Authorize]
    public class CompareController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _WHE;
        private readonly string _VoiceStatementPath;
        private readonly string _VoiceRecordPath;

        public CompareController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment WHE)
        {
            _context = context;
            _userManager = userManager;
            _WHE = WHE;
            _VoiceStatementPath = $"{_WHE.WebRootPath}/Voices/UserStatementVoices";
            _VoiceRecordPath = $"{_WHE.WebRootPath}/Voices/TestVoices";
        }

        [HttpGet]
        public IActionResult CompareVoices()
        {
            CompareViewModel viewModel = new()
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
        public async Task<IActionResult> CompareVoices(CompareViewModel model)
        {
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            string voice1Path = null, voice2Path = null;

            if (model.VoiceFile != null)
            {
                var userStatement = $"{Guid.NewGuid()}{Path.GetExtension(model.VoiceFile.FileName)}";
                var path = Path.Combine(_VoiceStatementPath, userStatement);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.VoiceFile.CopyToAsync(stream);
                }
                voice1Path = path;
            }

            if (model.RecordFile != null)
            {
                var userRecord = $"{Guid.NewGuid()}{Path.GetExtension(model.RecordFile.FileName)}";
                var path2 = Path.Combine(_VoiceRecordPath, userRecord);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    await model.RecordFile.CopyToAsync(stream);
                }
                voice2Path = path2;
            }

            var result = await CompareVoicesAsync(voice1Path, voice2Path, 0.998996455946643);
            ViewData["Result"] = result;

            return View(model);
        }

        private async Task<string> CompareVoicesAsync(string voice1Path, string voice2Path, double threshold)
        {
            using (var client = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    form.Add(new StringContent(threshold.ToString()), "threshold");
                    form.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(voice1Path)), "voice1", Path.GetFileName(voice1Path));
                    form.Add(new ByteArrayContent(System.IO.File.ReadAllBytes(voice2Path)), "voice2", Path.GetFileName(voice2Path));

                    var response = await client.PostAsync("http://localhost:5000/compare", form);
                    response.EnsureSuccessStatusCode();

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonObject = JObject.Parse(jsonString);

                    bool result = jsonObject.Value<bool>("result");
                    double similarity = jsonObject.Value<double>("similarity");

                    return $"Result: {(result ? "Similar" : "Not Similar")}, Percentage: {similarity * 100:0.00}%";
                }
            }
        }

    }




    /*  public class CompareController : Controller
      {
          private readonly ApplicationDbContext _context;
          private readonly UserManager<ApplicationUser> _userManager;
          private readonly IWebHostEnvironment _webHostEnvironment;
          private readonly ILogger<CompareController> _logger;
          private readonly string _voiceStatementPath;
          private readonly string _voiceRecordPath;

          public CompareController(ApplicationDbContext context,
                                   UserManager<ApplicationUser> userManager,
                                   IWebHostEnvironment webHostEnvironment,
                                   ILogger<CompareController> logger)
          {
              _context = context;
              _userManager = userManager;
              _webHostEnvironment = webHostEnvironment;
              _logger = logger;
              _voiceStatementPath = Path.Combine(_webHostEnvironment.WebRootPath, "Voices", "UserStatementVoices");
              _voiceRecordPath = Path.Combine(_webHostEnvironment.WebRootPath, "Voices", "TestVoices");
          }

          [HttpGet]
          public IActionResult CompareVoices()
          {
              var viewModel = new CompareViewModel
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
          public async Task<IActionResult> CompareVoices(CompareViewModel model)
          {
              if (!ModelState.IsValid)
              {
                  _logger.LogWarning("Model state is invalid.");
                  return View(model);
              }

              var currentUser = await _userManager.GetUserAsync(User);
              if (currentUser == null)
              {
                  _logger.LogWarning("User not found.");
                  return Unauthorized();
              }

              var userStatementPath = await SaveUploadedFileAsync(model.VoiceFile, _voiceStatementPath);
              var userRecordPath = await SaveUploadedFileAsync(model.RecordFile, _voiceRecordPath);

              if (string.IsNullOrEmpty(userStatementPath) || string.IsNullOrEmpty(userRecordPath))
              {
                  _logger.LogWarning("Both voice files must be provided.");
                  return BadRequest("Both voice files must be provided.");
              }

              var userStatement = new UserStatement
              {
                  VoiceFile = userStatementPath,
                  CreatedDate = DateTime.Now,
                  ApplicationUser = currentUser
              };
              _context.UserStatements.Add(userStatement);

              var userRecord = new UserRecord
              {
                  RecordFile = userRecordPath,
                  RecordsDate = DateTime.Now,
                  ApplicationUser = currentUser
              };
              _context.UserRecords.Add(userRecord);

              await _context.SaveChangesAsync();

              var comparisonResult = await CompareVoiceFilesAsync(userStatementPath, userRecordPath);

              if (comparisonResult != null)
              {
                  ViewBag.Message = comparisonResult.result ? "The voices are similar." : "The voices are not similar.";
                  ViewBag.Similarity = comparisonResult.similarity;
              }
              else
              {
                  ViewBag.Error = "Error comparing voices.";
              }

              return View("CompareVoices");
          }

          private async Task<string> SaveUploadedFileAsync(IFormFile file, string destinationPath)
          {
              if (file == null)
              {
                  return null;
              }

              Directory.CreateDirectory(destinationPath); // Ensure the directory exists

              var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
              var fullPath = Path.Combine(destinationPath, fileName);

              try
              {
                  using (var stream = new FileStream(fullPath, FileMode.Create))
                  {
                      await file.CopyToAsync(stream);
                  }
                  _logger.LogInformation($"File saved successfully to {fullPath}");
              }
              catch (Exception ex)
              {
                  _logger.LogError($"Error saving file to {fullPath}: {ex.Message}");
                  return null;
              }

              return fullPath;
          }

          private async Task<dynamic> CompareVoiceFilesAsync(string voice1Path, string voice2Path)
          {
              try
              {
                  using (var httpClient = new HttpClient())
                  {
                      using (var form = new MultipartFormDataContent())
                      {
                          form.Add(new StreamContent(new FileStream(voice1Path, FileMode.Open)), "voice1", Path.GetFileName(voice1Path));
                          form.Add(new StreamContent(new FileStream(voice2Path, FileMode.Open)), "voice2", Path.GetFileName(voice2Path));
                          form.Add(new StringContent("0.5"), "threshold");

                          var response = await httpClient.PostAsync("http://localhost:5000/compare", form);

                          if (response.IsSuccessStatusCode)
                          {
                              var responseData = await response.Content.ReadAsStringAsync();
                              return JsonConvert.DeserializeObject<dynamic>(responseData);
                          }
                          else
                          {
                              _logger.LogError($"Error in voice comparison API response: {response.StatusCode}");
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  _logger.LogError($"Exception during voice comparison: {ex.Message}");
              }

              return null;
          }
      }*/
}


/*
using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace IsFake.Controllers.User
{
    public class CompareController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _voiceStatementPath;
        private readonly string _voiceRecordPath;

        public CompareController(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _voiceStatementPath = Path.Combine(_webHostEnvironment.WebRootPath, "Voices", "UserStatementVoices");
            _voiceRecordPath = Path.Combine(_webHostEnvironment.WebRootPath, "Voices", "TestVoices");
        }

        [HttpGet]
        public async Task<IActionResult> CompareVoices()
        {
            var viewModel = new CompareViewModel
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
        public async Task<IActionResult> CompareVoices(CompareViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var userStatementPath = await SaveUploadedFileAsync(model.VoiceFile, _voiceStatementPath);
            var userRecordPath = await SaveUploadedFileAsync(model.RecordFile, _voiceRecordPath);

            if (string.IsNullOrEmpty(userStatementPath) || string.IsNullOrEmpty(userRecordPath))
            {
                return BadRequest("Both voice files must be provided.");
            }

            var userStatement = new UserStatement
            {
                VoiceFile = userStatementPath,
                CreatedDate = DateTime.Now,
                ApplicationUser = currentUser
            };
            _context.UserStatements.Add(userStatement);

            var userRecord = new UserRecord
            {
                RecordFile = userRecordPath,
                RecordsDate = DateTime.Now,
                ApplicationUser = currentUser
            };
            _context.UserRecords.Add(userRecord);

            await _context.SaveChangesAsync();

            var comparisonResult = await CompareVoiceFilesAsync(userStatementPath, userRecordPath);

            if (comparisonResult != null)
            {
                ViewBag.Message = comparisonResult.result ? "The voices are similar." : "The voices are not similar.";
                ViewBag.Similarity = comparisonResult.similarity;
            }
            else
            {
                ViewBag.Error = "Error comparing voices.";
            }

            return View("CompareVoices");
        }

        private async Task<string> SaveUploadedFileAsync(IFormFile file, string destinationPath)
        {
            if (file == null)
            {
                return null;
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(destinationPath, fileName);

            Directory.CreateDirectory(destinationPath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fullPath;
        }

        private async Task<dynamic> CompareVoiceFilesAsync(string voice1Path, string voice2Path)
        {
            using (var httpClient = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    form.Add(new StreamContent(new FileStream(voice1Path, FileMode.Open)), "voice1", Path.GetFileName(voice1Path));
                    form.Add(new StreamContent(new FileStream(voice2Path, FileMode.Open)), "voice2", Path.GetFileName(voice2Path));
                    form.Add(new StringContent("0.5"), "threshold");

                    var response = await httpClient.PostAsync("http://localhost:5000/compare", form);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<dynamic>(responseData);
                    }
                }
            }

            return null;
        }
    }
}
*/



/*
using IsFakeModels;
using IsFakeRepository;
using IsFakeViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;

namespace IsFake.Controllers.User
{
    public class CompareController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserStatement _userStatement;
        public readonly UserRecord _userRecord;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _WHE;
        private readonly string _VoiceStatementPath;
        private readonly string _VoiceRecordPath;
        public CompareController(ApplicationDbContext context,
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
            public async Task<IActionResult> CompareVoices()
            {
            CompareViewModel viewModel = new()
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

        public async Task<IActionResult> CompareVoices(CompareViewModel model)
        {
            
            // Save uploaded files
            ApplicationUser currentUser = await _userManager.GetUserAsync(User);
            string voice1Path = null, voice2Path = null;

            if (model.VoiceFile != null)
            {
                var userStatement = $"{Guid.NewGuid()}{Path.GetExtension(model.VoiceFile.FileName)}";
                var path = Path.Combine(_VoiceStatementPath, userStatement);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.VoiceFile.CopyToAsync(stream);
                }
                voice1Path = path;
            }
            UserStatement UserStatementObj = new UserStatement
            {
                VoiceFile = voice1Path,
                CreatedDate = DateTime.Now,
                ApplicationUser = currentUser,
            };

            _context.UserStatements.Add(UserStatementObj);
            _context.SaveChanges();



            if (model.RecordFile != null)
            {
                var userRecord = $"{Guid.NewGuid()}{Path.GetExtension(model.RecordFile.FileName)}";
                var path2 = Path.Combine(_VoiceRecordPath, userRecord);
                using (var stream = new FileStream(path2, FileMode.Create))
                {
                    await model.RecordFile.CopyToAsync(stream);
                }
                voice2Path = path2;
            }
            UserRecord UserRecordObj = new UserRecord
            {
                RecordFile = voice2Path,
                RecordsDate = DateTime.Now,
                ApplicationUser = currentUser,
            };

            _context.UserRecords.Add(UserRecordObj);
            _context.SaveChanges();



            if (voice1Path == null || voice2Path == null)
            {
                return BadRequest("Both voice files must be provided.");
            }

            // Call Flask API
            using (var httpClient = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    form.Add(new StreamContent(new FileStream(voice1Path, FileMode.Open)), "voice1", Path.GetFileName(voice1Path));
                    form.Add(new StreamContent(new FileStream(voice2Path, FileMode.Open)), "voice2", Path.GetFileName(voice2Path));
                    form.Add(new StringContent("0.5"), "threshold");

                    HttpResponseMessage response = await httpClient.PostAsync("http://localhost:5000/compare", form);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<dynamic>(responseData);

                        if (result.result == true)
                        {
                            ViewBag.Message = "The voices are similar.";
                        }
                        else
                        {
                            ViewBag.Message = "The voices are not similar.";
                        }

                        ViewBag.Similarity = result.similarity;
                    }
                    else
                    {
                        ViewBag.Error = "Error comparing voices.";
                    }
                }
            }

            return View("CompareVoices");
        }
    }
}

*/