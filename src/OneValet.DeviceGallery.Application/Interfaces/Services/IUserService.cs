using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<UserResponse>> AddUserAsync(UserRequest userRequest);
        Task UpdateUserAsync(int id, UserRequest userRequest);
        Task<Response<UserResponse>> GetUserByIdAsync(int id);
        Task<Response<IEnumerable<UserResponse>>> GetAllUsersAsync();
        Task DeleteUserAsync(int id);
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> BasicAuthenticateAsync(AuthenticationRequest request);
    }
}
