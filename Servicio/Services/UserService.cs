using AutoMapper;
using Modelo.DTO;
using Modelo.Models;
using Modelo.ViewModels;
using Servicio.Helper;
using Servicio.IServices;
using Servicio.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Services
{
    public class UserService : IUserService
    {
        private readonly EcommerceContext _context;
        private readonly IMapper _mapper;

        public UserService(EcommerceContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<UserDTO> GetUsers()
        {
            return _mapper.Map<List<UserDTO>>(_context.User.ToList());
        }

        public UserDTO GetUserById(int id)
        {
            var user = _context.User.Where(w => w.Id == id).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(user);
        }

        public string CreateUserWithRole(UserViewModel User)
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

            Role? role = _context.Role.FirstOrDefault(x => x.Id == User.RoleId); if (role == null)
            {
                return "Role ID doesn't exist";
            }

            _context.User.Add(new User()
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Email = User.Email,
                Password = User.Password.GetSHA256(),
                RoleId = _context.Role.First(f => f.Id == User.RoleId).Id
            });
            _context.SaveChanges();

            return "User created successfully";
        }

        public UserDTO UpdateUserRole(int id, int roleId)
        {
            var existingUser = _context.User.FirstOrDefault(p => p.Id == id);

            if (existingUser == null)
            {
                return null;
            }

            var existingRole = _context.Role.FirstOrDefault(r => r.Id == roleId);

            if (existingRole == null)
            {
                return null;
            }

            existingUser.RoleId = roleId;
            _context.SaveChanges();

            return _mapper.Map<UserDTO>(_context.User.FirstOrDefault(w => w.Id == id));
        }

        public string DeleteUser(int id)
        {
            var user = _context.User.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return null;
            }
            
            _context.User.Remove(_context.User.Single(s => s.Id == id));
            _context.SaveChanges();

            return "User has been deleted successfully";

        }
    }
}
