using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.Helper;
using Servicio.IServices;
using Servicio.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Services
{
    public class PublicUserService : IPublicUserService
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public PublicUserService(EcommerceContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();   
        }

        public string ChangePassword(string currentPassword, string newPassword, int userId)
        {
            var user =  _context.User.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            if (user.Password != currentPassword.GetSHA256())
            {
                throw new Exception("Invalid current password");
            }

            user.Password = newPassword.GetSHA256();
            _context.SaveChanges();

            return "Password change successful";
        }

        public UserDTO ChangeInfo(ChangeInfoViewModel model, int userId)
        {
            var existingUser = _context.User.FirstOrDefault(p => p.Id == userId);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.FirstName = model.FirstName;
            existingUser.LastName = model.LastName;
            existingUser.Email = model.Email;
            
            _context.SaveChanges();

            return _mapper.Map<UserDTO>(_context.User.Where(w => w.Id == userId).First());
        }
    }
}
