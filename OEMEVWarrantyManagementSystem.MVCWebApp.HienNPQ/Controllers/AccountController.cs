using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace OEMEVWarrantyManagementSystem.MVCWebApp.HienNPQ.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IHttpClientFactory httpClientFactory, ILogger<AccountController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        public sealed record LoginViewModel(string UserName, string Password);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var client = _httpClientFactory.CreateClient("ApiClient");
                var response = await client.PostAsJsonAsync("SystemUserAccounts/Login", new { model.UserName, model.Password });

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }

                var token = await response.Content.ReadFromJsonAsync<string>();
                if (string.IsNullOrWhiteSpace(token))
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }

                // Decode JWT to extract claims
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var claims = jwt.Claims
                    .Where(c => c.Type == ClaimTypes.Name || c.Type == ClaimTypes.Role)
                    .ToList();

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Save token for API calls
                HttpContext.Session.SetString("JwtToken", token);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error");
                ModelState.AddModelError("", "Unexpected error during login.");
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("JwtToken");
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Forbidden() => View();
    }
}
