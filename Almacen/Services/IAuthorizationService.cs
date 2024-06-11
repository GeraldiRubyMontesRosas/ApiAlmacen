using Almacen.DTOs;

namespace Almacen.Services
{
    public interface IAuthorizationService
    {
        Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto);
        Task Logout(int userId);
    }
}
