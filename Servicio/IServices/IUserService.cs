using Modelo.DTO;
using Modelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.IServices
{
    public interface IUserService
    {
        List<UserDTO> GetUsers();
        UserDTO GetUserById(int id);
        string CreateUserWithRole(UserViewModel User);
        UserDTO UpdateUserRole(int id, int roleId);
        string DeleteUser(int id);
    }
}
