using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OffShoreAspNetBoilerplate.Models.TokenAuth;

namespace OffShoreAspNetBoilerplate.Web.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController]
    public class TokenAuthController : Controller
    {
        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
        {
            var accessToken = CreateAccessToken(CreateJwtClaims(new ClaimsIdentity()));

            return await Task.FromResult(new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = "e06f495d-82ea-4e4e-b52f-649d9077666c",
                ExpireInSeconds = 99,
                UserId = 1
            });
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "issuer",
                audience: "Audience",
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("OffShoreAspNetBoilerplate_C421AAEE0D114E9C")), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }
    }
}
