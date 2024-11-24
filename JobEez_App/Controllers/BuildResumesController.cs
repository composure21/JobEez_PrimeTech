using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobEez_App.Models;
using System.Security.Claims;
using System.Text;

namespace JobEez_App.Controllers
{
    public class BuildResumesController : Controller
    {
        private readonly JobEezPrimeTechContext _context;
        private readonly ILogger<AccountController> _logger;
        public BuildResumesController(JobEezPrimeTechContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: BuildResumes
        public async Task<IActionResult> Index()
        {
            //return View(await _context.BuildResumes.ToListAsync());
            var resumes = await _context.BuildResumes.ToListAsync();
            return View(resumes);
        }

        // GET: BuildResumes/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildResume = await _context.BuildResumes
                .FirstOrDefaultAsync(m => m.PersonalInfoId == id);
            if (buildResume == null)
            {
                return NotFound();
            }

            return View(buildResume);
        }

        // GET: BuildResumes/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,JobTitle1,JobTitle2,Company,Company1,Company2,WorkLocation,WorkLocation1,WorkLocation2,WorkerStartDate,WorkerStartDate1,WorkerStartDate2,WorkerEndDate,WorkerEndDate1,WorkerEndDate2,Responsibilities,Responsibilities1,Responsibilities2,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
        {
            // Set the user_id from the currently logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "User ID could not be determined. Please log in and try again.");
                return View(buildResume); // Return view with the current model data
            }

            buildResume.UserId = userId; // Ensure UserId is set

            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "You must be logged in to create a resume.");
                return View(buildResume);
            }
            // Check if Responsibilities is populated
            if (string.IsNullOrWhiteSpace(buildResume.Responsibilities))
            {
                ModelState.AddModelError("Responsibilities", "Responsibilities are required.");
                return View(buildResume);
            }

            // Add the new BuildResume entry to the context
            _context.Add(buildResume);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = buildResume.PersonalInfoId });
            }
            catch (DbUpdateException ex)
            {
                // Log the error details
                Console.WriteLine($"Database update error: {ex.Message}");
                ModelState.AddModelError("", "Unable to save changes. Please try again later.");
            }
            catch (Exception ex)
            {
                // Log unexpected error details
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            }

            // If we get to this point, something went wrong, return the view with model errors
            return View(buildResume);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            Console.WriteLine($"UserId: {userId}, PersonalInfoId: {id}"); 

            // Fetch the resume associated with the userId
            var resume = _context.BuildResumes.FirstOrDefault(r => r.UserId == userId);

            if (resume == null)
            {
                return RedirectToAction("Create", "BuildResumes"); // Handle case where CV does not exist
            }

            return View(resume);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BuildResume model)
        {
            if (ModelState.IsValid)
            {
                // Get the current user ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Fetch the resume associated with the userId
                var existingResume = _context.BuildResumes.FirstOrDefault(r => r.UserId == userId);

                if (existingResume != null)
                {
                    // Update existing resume fields
                    existingResume.FullName = model.FullName;
                    existingResume.PhoneNumber = model.PhoneNumber;
                    existingResume.Address = model.Address;
                    existingResume.LinkedinUrl = model.LinkedinUrl;
                    existingResume.PhotoUrl = model.PhotoUrl;
                    existingResume.CareerObjective = model.CareerObjective;
                    existingResume.Degree = model.Degree;
                    existingResume.Institution = model.Institution;
                    existingResume.EdLocation = model.EdLocation;
                    existingResume.GraduationYear = model.GraduationYear;
                    existingResume.RelevantCoursesAchievements = model.RelevantCoursesAchievements;

                    //work experience
                    existingResume.JobTitle = model.JobTitle;
                    existingResume.Company = model.Company;
                    existingResume.WorkLocation = model.WorkLocation;
                    existingResume.WorkerStartDate = model.WorkerStartDate;
                    existingResume.WorkerEndDate = model.WorkerEndDate;
                    existingResume.Responsibilities = model.Responsibilities;
                    //work experience 1
                    existingResume.JobTitle1 = model.JobTitle1;
                    existingResume.Company1 = model.Company1;
                    existingResume.WorkLocation1 = model.WorkLocation1;
                    existingResume.WorkerStartDate1 = model.WorkerStartDate1;
                    existingResume.WorkerEndDate1 = model.WorkerEndDate1;
                    existingResume.Responsibilities1 = model.Responsibilities1;

                    //work experience 2
                    existingResume.JobTitle2 = model.JobTitle2;
                    existingResume.Company2 = model.Company2;
                    existingResume.WorkLocation2 = model.WorkLocation2;
                    existingResume.WorkerStartDate2 = model.WorkerStartDate2;
                    existingResume.WorkerEndDate2 = model.WorkerEndDate2;
                    existingResume.Responsibilities2 = model.Responsibilities2;


                    existingResume.SkillName = model.SkillName;
                    existingResume.SkillType = model.SkillType;
                    existingResume.Language = model.Language;
                    existingResume.Proficiency = model.Proficiency;
                    existingResume.CertificationName = model.CertificationName;
                    existingResume.VolunteerRole = model.VolunteerRole;
                    existingResume.Organization = model.Organization;
                    existingResume.VolunteerStartDate = model.VolunteerStartDate;
                    existingResume.VolunteerEndDate = model.VolunteerEndDate;
                    existingResume.VolunteerResponsibilities = model.VolunteerResponsibilities;

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action after successful update
                    return RedirectToAction(nameof(Index)); // Redirect to the appropriate action
                }
            }

            return View(model);
        }

        // GET: BuildResumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildResume = await _context.BuildResumes
                .FirstOrDefaultAsync(m => m.PersonalInfoId == id);
            if (buildResume == null)
            {
                return NotFound();
            }

            return View(buildResume);
        }

        // POST: BuildResumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildResume = await _context.BuildResumes.FindAsync(id);
            if (buildResume != null)
            {
                _context.BuildResumes.Remove(buildResume);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildResumeExists(int id)
        {
            return _context.BuildResumes.Any(e => e.PersonalInfoId == id);
        }

        // GET: BuildResumes/Register
        public IActionResult Register()
        {
            return View(); // Create a view named Register.cshtml in the corresponding Views/BuildResumes folder
        }
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> PayFastReturn()
        {
            try
            {
                // Retrieve the form data from PayFast
                var formData = await Request.ReadFormAsync();

                // Log the form data for debugging purposes
                _logger.LogInformation("PayFast Return received: {FormData}", formData);

                // Prepare data for validation request
                var queryParams = new Dictionary<string, string>
        {
            { "pf_payment_id", formData["pf_payment_id"] },
            { "pf_order_id", formData["pf_order_id"] },
            { "payment_status", formData["status"] },
            { "amount_gross", formData["amount_gross"] },
            { "amount_fee", formData["amount_fee"] },
            { "amount_settlement", formData["amount_settlement"] },
            { "item_name", formData["item_name"] },
            { "item_description", formData["item_description"] },
            { "payment_date", formData["payment_date"] },
            { "payment_time", formData["payment_time"] },
            { "token", formData["token"] },
            { "passphrase", "bcad3will2024" } // Replace with your passphrase
        };

                // Create the query string for validation
                var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

                // Send the validation request to PayFast
                var validationUrl = "https://sandbox.payfast.co.za/eng/query/validate";
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(validationUrl, new StringContent(queryString, Encoding.UTF8, "application/x-www-form-urlencoded"));

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation("PayFast validation response: {Response}", result);

                        // Check if payment is successful
                        if (result.Contains("VALID"))
                        {
                            // Payment is valid, proceed with processing
                            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.PayFastPaymentId == formData["pf_payment_id"]);
                            if (user != null)
                            {
                                user.HasPaid = true;
                                user.PaymentDate = DateTime.UtcNow;
                                _context.Update(user);
                                await _context.SaveChangesAsync();

                                _logger.LogInformation("Payment successfully processed for user: " + user.UserName);
                                return RedirectToAction("PaymentSuccess");
                            }
                        }
                        else
                        {
                            _logger.LogWarning("Payment validation failed. Response: {Response}", result);
                            return RedirectToAction("PaymentFailed");
                        }
                    }
                    else
                    {
                        _logger.LogError("PayFast validation request failed. Status code: {StatusCode}", response.StatusCode);
                        return RedirectToAction("PaymentFailed");
                    }
                }

                // Fallback return if no condition is met (though this case should not happen if all paths are handled)
                return RedirectToAction("PaymentFailed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the PayFast return.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // POST: BuildResumes/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,Company,WorkLocation,WorkerStartDate,WorkerEndDate,Responsibilities,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildResume);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to the Index action after successful registration
            }
            return View(buildResume);
        }
        public IActionResult AvailableCVs(string searchTerm)
        {
            var cvs = _context.BuildResumes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                cvs = cvs.Where(cv =>
                    cv.FullName.Contains(searchTerm) ||
                    cv.JobTitle.Contains(searchTerm) ||
                    cv.Company.Contains(searchTerm));
            }

            var model = cvs.ToList();
            return View(model);
        }


    }
}
