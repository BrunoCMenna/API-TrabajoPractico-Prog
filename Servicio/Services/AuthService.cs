using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modelo.Helper;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.Helper;
using Servicio.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Services
{
    public class AuthService : IAuthService
    {
        private readonly EcommerceContext _context;
        private readonly AppSettings _appSettings;

        public AuthService(EcommerceContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public string SignIn(SignInViewModel User)
        {
            if (string.IsNullOrEmpty(User.Email))
            {
                return "Email is required";
            }

            if (string.IsNullOrEmpty(User.Password))
            {
                return "Password is required";
            }

            User? user = _context.User.FirstOrDefault(x => x.Email == User.Email);

            if (user != null)
            {
                return "Email is already in use";
            }

            _context.User.Add(new User()
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Password = User.Password.GetSHA256(),
                RoleId = 1
            });
            _context.SaveChanges();

            string response = GetToken(_context.User.OrderBy(x => x.Id).Last());

            return response;
        }

        public string Login(LoginViewModel User)
        {
            User? user = _context.User.FirstOrDefault(x => x.Email == User.Email && x.Password == User.Password.GetSHA256());

            if (user == null)
            {
                return string.Empty;
            }

            return GetToken(user);
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Surname, user.LastName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, _context.Role.First(x => x.Id == user.RoleId).Description)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
