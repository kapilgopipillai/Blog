using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Domain.User;
using Blog.Model;
using Blog.Model.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly JWTSettings _jwtsettings;
        private readonly IUserReader _userReader;
        
        public LoginController(IOptions<JWTSettings> jwtsettings, IUserReader userReader)
        {
            _jwtsettings = jwtsettings.Value;
            _userReader = userReader;
        }
        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserWithToken>> Login([FromBody] LoginModel login)
        {
            try
            {
                RegistrationModel user = new RegistrationModel();
                user = await _userReader.Login(login.UserName, login.UserPassword);

                UserWithToken userWithToken = null;

                if (user != null)
                {
                    RefreshToken refreshToken = GenerateRefreshToken();

                    refreshToken.UserId = user.Id;
                    //await _userWriter.UpdateRefreshToken(refreshToken);

                    userWithToken = new UserWithToken(user);
                    //userWithToken.RefreshToken = refreshToken.Token;
                }

                //RefreshToken refreshToken = GenerateRefreshToken();
                //refreshToken.UserId = user.Id;
                //await _userWriter.UpdateRefreshToken(refreshToken);

                //if (userWithToken == null)
                //{
                //    return NotFound();
                //}

                //sign your token here here..
                userWithToken.AccessToken = GenerateAccessToken(user);
                return userWithToken;
            }

            catch (Exception ex)
            { throw ex; }

        }
        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(1);
            refreshToken.Createddate = DateTime.UtcNow;
            refreshToken.CreatedByIp = ipAddress();
            return refreshToken;
        }
        private string GenerateAccessToken(RegistrationModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userid", Convert.ToString(user.Id)),
                    new Claim(ClaimTypes.Name, Convert.ToString(user.EmailAddress))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
