using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OffShoreAspNetBoilerplate.Models.TokenAuth;

namespace OffShoreAspNetBoilerplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenAuthController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Authenticate([FromBody] AuthenticateModel model)
        {
            return await Task.FromResult(Json(new AuthenticateResultModel
            {
                AccessToken = new Guid().ToString(),
                EncryptedAccessToken = new Guid().ToString(),
                ExpireInSeconds = 99,
                UserId = 1
            }));
        }
    }
}
