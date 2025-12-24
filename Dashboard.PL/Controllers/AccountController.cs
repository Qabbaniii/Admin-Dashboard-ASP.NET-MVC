using Dashboard.DAL.Models.Identity;
using Dashboard.PL.Helper;
using Dashboard.PL.ViewModels.IdentityVMs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Net;
using System.Security.Claims;

namespace Dashboard.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager ,IEmailSettings emailSettings):Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
            };
            var Result = userManager.CreateAsync(user, model.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
        }

        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                bool flag = userManager.CheckPasswordAsync(user, model.Password).Result;
                if (flag)
                {
                    var Result = signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Access Denied");
                    if (Result.Succeeded)
                        return RedirectToAction("index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login");
            return View(model);

        }
        #endregion

        #region Signout
        public IActionResult SignOut()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction("Login");
        }
        #endregion

        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = Token }, protocol: HttpContext.Request.Scheme);

                    var email = new Email()
                    {
                        To = user.Email,
                        Subject = "Reset Your Password",
                        Body = $"<p>اضغط على الرابط لإعادة تعيين كلمة المرور:</p><a href='{resetLink}'>Reset Password</a>"


                    };
                    emailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is not Valid");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email,string Token)
        {
            TempData["Email"] = email;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                string Email = TempData["Email"] as string;
                string Token = TempData["Token"] as string;
                var user = await userManager.FindByEmailAsync(Email);

                var Result = await userManager.ResetPasswordAsync(user,Token,model.Password);

                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        #endregion

        #region Google Signin
        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(prop, GoogleDefaults.AuthenticationScheme);
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(
                GoogleDefaults.AuthenticationScheme
            );

            if (!result.Succeeded)
                return RedirectToAction("Login");

            var claims = result.Principal.Identities
                .FirstOrDefault()
                ?.Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
