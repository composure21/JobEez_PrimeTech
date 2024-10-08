using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobEez_App.Models;
using System.Security.Claims;

namespace JobEez_App.Controllers
{
    public class BuildResumesController : Controller
    {
        private readonly JobEezPrimeTechContext _context;

        public BuildResumesController(JobEezPrimeTechContext context)
        {
            _context = context;
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

        //// POST: BuildResumes/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,Company,WorkLocation,WorkerStartDate,WorkerEndDate,Responsibilities,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Assign the user_id from the current user
        //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Assuming you are using Identity
        //        buildResume.user_id = userId;

        //        _context.Add(buildResume);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Details", new { id = buildResume.PersonalInfoId });
        //    }
        //    return View(buildResume);
        //}
        // POST: BuildResumes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalInfoId,FullName,PhoneNumber,Address,LinkedinUrl,PhotoUrl,CareerObjective,Degree,Institution,EdLocation,GraduationYear,RelevantCoursesAchievements,JobTitle,Company,WorkLocation,WorkerStartDate,WorkerEndDate,Responsibilities,SkillName,SkillType,Language,Proficiency,CertificationName,VolunteerRole,Organization,VolunteerStartDate,VolunteerEndDate,VolunteerResponsibilities")] BuildResume buildResume)
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

            //// Check if the model state is valid
            //if (!ModelState.IsValid)
            //{
            //    // Log model state errors for debugging
            //    Console.WriteLine("Model state is invalid.");
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine(error.ErrorMessage); // Logging for debugging
            //    }
            //    return View(buildResume); // Return view with the current model data
            //}

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


        //// GET: BuildResumes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var buildResume = await _context.BuildResumes.FindAsync(id);
        //    if (buildResume == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(buildResume);
        //}
        // GET: BuildResumes/Edit
        // GET: BuildResumes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID
            Console.WriteLine($"UserId: {userId}, PersonalInfoId: {id}"); // Log for debugging

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
                    existingResume.JobTitle = model.JobTitle;
                    existingResume.Company = model.Company;
                    existingResume.WorkLocation = model.WorkLocation;
                    existingResume.WorkerStartDate = model.WorkerStartDate;
                    existingResume.WorkerEndDate = model.WorkerEndDate;
                    existingResume.Responsibilities = model.Responsibilities;
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

            // Return the model in case of errors
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
