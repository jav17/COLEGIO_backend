using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Colegio.Datos;
using Colegio.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Colegio.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IConfiguration _config;
        public LoginController(SchoolContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email.ToLower() == userLogin.email.ToLower());
            if (user == null)
            {
                return Unauthorized();
            }
            if (!VerifyPasswordHash(userLogin.password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }
            return Ok(new { token = GenerarTokenJWT(user) });
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            };
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHashBuffered, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var passwordHashNew = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashBuffered).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNew));
            };
        }

        private string GenerarTokenJWT(Datos.Models.Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);

            var dateUtcNow = DateTime.UtcNow;

            Int32 unixTimestamp1 = (Int32)(dateUtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 unixTimestamp2 = (Int32)(dateUtcNow.AddMinutes(5).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            var claims = new[]
            {
                // REQUIRED
                new Claim(JwtRegisteredClaimNames.Iss, _config["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _config["Jwt:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, unixTimestamp2.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, unixTimestamp1.ToString()),

                // EXTRA INFO
                new Claim(JwtRegisteredClaimNames.GivenName, user.Firstname),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.Lastname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("Role", user.Role.Name)
            };

            var payload = new JwtPayload(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                //Expira a las 24horas
                expires: DateTime.UtcNow.AddMinutes(5));

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
