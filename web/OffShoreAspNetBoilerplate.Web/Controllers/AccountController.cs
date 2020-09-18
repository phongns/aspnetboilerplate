using System.Threading.Tasks;
using Abp.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OffShoreAspNetBoilerplate.Web.Models.Account;

namespace OffShoreAspNetBoilerplate.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {

        }


        public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
        {
            if (returnUrl.IsNullOrWhiteSpace())
            {
                returnUrl = GetAppHomeUrl();
            }

            return View(new LoginFormViewModel
            {
                ReturnUrl = returnUrl,
                //IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                //IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                //MultiTenancySide = AbpSession.MultiTenancySide
            });
        }


        [HttpPost]
        //[UnitOfWork]
        public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            //returnUrl = NormalizeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            //var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, GetTenancyNameOrNull());

            //await _signInManager.SignInAsync(loginResult.Identity, loginModel.RememberMe);
            //await UnitOfWorkManager.Current.SaveChangesAsync();

            //AuthenticationManager.SignIn(
            //           new AuthenticationProperties
            //           {
            //               //IsPersistent = isPersistent
            //           },
            //           loginResult.Identity
            //       );

            return Json(new { TargetUrl = returnUrl });
        }

        #region Helpers

        public ActionResult RedirectToAppHome()
        {
            return RedirectToAction("Index", "Home");
        }

        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "Home");
        }

        #endregion
    }
}
