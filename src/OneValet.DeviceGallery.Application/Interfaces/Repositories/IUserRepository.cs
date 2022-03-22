using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(DeviceUser user);
        Task<DeviceUser> GetUserByIdAsync(int id);
        Task<DeviceUser> GetUserByUserNameAsync(string userName);
        Task<DeviceUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<DeviceUser>> GetAllUsersAsync();
        void UpdateUser(DeviceUser user);
        void DeleteUser(DeviceUser user);
        Task<bool> UserCredentialExist(AuthenticationRequest request);
    }
}
