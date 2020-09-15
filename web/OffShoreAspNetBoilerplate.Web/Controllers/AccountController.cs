using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OffShoreAspNetBoilerplate.Web.Controllers
{

    public class AccountController : Controller
    {

        public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
        {
            return Content("Login");
        }
    }
}
