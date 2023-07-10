using Modelo.DTO;
using Modelo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.IServices
{
    public interface IPublicUserService
    {
        string ChangePassword(string currentPassword, string newPassword, int userId);
        UserDTO ChangeInfo(ChangeInfoViewModel model, int userId);
    }
}
