using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SIG.Data.Entity.Identity;
using SIG.Model.Admin.InputModel.Identity;
using SIG.Services.Identity;

namespace SIG.SIGCMS.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Token")]
    public class TokenController : Controller
    {
      
        private readonly ILogger<TokenController> _logger;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly TokenOptions _tokenOptions;
        public TokenController( IUserServices userServices, IRoleServices roleServices, IOptions<TokenOptions> tokens, ILogger<TokenController> logger)
        {
            
            _userServices = userServices;
            _roleServices = roleServices;
            _tokenOptions = tokens.Value;
            _logger = logger;


        }
        [HttpPost]
        public IActionResult Token([FromBody] LoginIM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _userServices.SignIn(model.Username, model.Password); ;

            if (user == null)
            {
                return BadRequest();
            }


            var token = GetJwtSecurityToken(user);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
        private JwtSecurityToken GetJwtSecurityToken(User user)
        {
            // var userClaims = await _userManager.GetClaimsAsync(user);
            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim("RealName", user.RealName??"无"),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var userRoles = _roleServices.GetRolesByUserId(user.Id).ToArray();
            //add a list of roles

            if (userRoles.Any())
            {
                var roles = string.Join(",", userRoles.Select(d => d.RoleName));
                claims.Add(new Claim(ClaimTypes.Role, roles));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );
        }
    }
}