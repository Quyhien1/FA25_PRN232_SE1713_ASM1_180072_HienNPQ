using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OEMEVWarrantyManagementSystem.Service.HienNPQ;
using System.Security.Claims;

namespace OEMEVWarrantyManagementSystem.MVCWebApp.HienNPQ.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SystemUserAccountService _userService;

        public AccountController(SystemUserAccountService userService)
        {
            _userService = userService;
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

            var user = await _userService.GetUserAccount(model.UserName, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            // Build claims (make sure Role claim matches what your [Authorize(Roles="1,2")] expects)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()) // roles "1","2" etc.
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home"); // or another landing page
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult Forbidden() => View();
    }
}
