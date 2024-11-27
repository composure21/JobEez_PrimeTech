using JobEez_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Text;

namespace JobEez_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IPasswordHasher<AspNetUser> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        private readonly JobEezPrimeTechContext _context;

        public AccountController(SignInManager<AspNetUser> signInManager,
                                 UserManager<AspNetUser> userManager,
                                 IPasswordHasher<AspNetUser> passwordHasher, IConfiguration configuration, ILogger<AccountController> logger, JobEezPrimeTechContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _logger = logger;
            _context = context;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "BuildResumes"); // Redirect to a successful landing page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        // GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //Code Attribution:https://developers.payfast.co.za/docs

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AspNetUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role
            };

            var result = await _signInManager.UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Assign role
                await _signInManager.UserManager.AddToRoleAsync(user, model.Role);

                // Only require payment for Employers
                if (model.Role == "Employer")
                {
                    // Set payment amount and description for Employer
                    string amount = "50.00";
                    string itemName = "Employer Registration Fee";

                    // Redirect to PayFast for payment
                    var payFastUrl = "https://sandbox.payfast.co.za/eng/process"; // Sandbox URL
                    var queryParams = new Dictionary<string, string>
            {
                { "email_address", model.Email },
                { "merchant_id", _configuration["PayFast:MerchantId"] },
                { "merchant_key", _configuration["PayFast:MerchantKey"] },
                { "amount", amount },
                { "item_name", itemName },
                { "return_url", Url.Action("Index", "BuildResumes", null, Request.Scheme) },
                { "cancel_url", Url.Action("PaymentCancel", "Account", null, Request.Scheme) },
                { "notify_url", Url.Action("PayFastReturn", "Account", null, Request.Scheme) }
            };

                    var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));
                    return Redirect($"{payFastUrl}?{queryString}");
                }
                else
                {
                    // No payment needed for Employees, just redirect to BuildResumes
                    return RedirectToAction("Index", "BuildResumes");
                }
            }

            // Handle errors if account creation failed
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        //Code Attribution:https://developers.payfast.co.za/docs

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

        //Code Attribution:https://developers.payfast.co.za/docs


        public async Task<IActionResult> UpdatePaymentId()
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (user != null)
            {
                user.PayFastPaymentId = "some_payment_id"; // Assign the PayFast ID
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult PayFastCancel()
        {
            TempData["ErrorMessage"] = "Payment was canceled.";
            return RedirectToAction("Index", "BuildResumes");
        }

        // Logout action
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "BuildResumes");
        }
        private bool ValidatePayFastSignature(IFormCollection formData)
        {
            var key = "bcad3wil2024"; 
            var signature = formData["signature"];
            var data = new List<string>
            {
                "amount_gross=" + formData["amount_gross"],
                "amount_fee=" + formData["amount_fee"],
                "amount_settlement=" + formData["amount_settlement"],
                "item_name=" + formData["item_name"],
                "item_description=" + formData["item_description"],
                "payment_date=" + formData["payment_date"],
                "payment_time=" + formData["payment_time"],
                "payment_status=" + formData["status"],
                "pf_payment_id=" + formData["pf_payment_id"],
                "pf_order_id=" + formData["pf_order_id"],
                "token=" + formData["token"]
            };

            // Add passphrase to the data string
            data.Add("passphrase=" + key);

           
            var dataString = string.Join("&", data);
            var computedSignature = GenerateSignature(dataString);

            
            return computedSignature == signature;
        }

        // Method to generate the signature using the concatenated data
        private string GenerateSignature(string dataString)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataString));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
