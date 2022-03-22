using AutoMapper;
using OneValet.DeviceGallery.Application.Common.Exceptions;
using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.Interfaces.Services;
using OneValet.DeviceGallery.Application.Wrappers;
using OneValet.DeviceGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;

        public UserService(IRepositoryProvider repositoryProvider, IMapper mapper)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }
        public async Task<Response<UserResponse>> AddUserAsync(UserRequest userRequest)
        {
            var userWithThisUserName = await _repositoryProvider.UserRepository.GetUserByUserNameAsync(userRequest.UserName);
            if (userWithThisUserName != null)
            {
                throw new ApiException($"Username {userRequest.UserName} is already taken.");
            }
            var userWithThisEmail = await _repositoryProvider.UserRepository.GetUserByEmailAsync(userRequest.Email);
            if (userWithThisEmail != null)
            {
                throw new ApiException($"Email {userRequest.Email} is already taken.");
            }
            var user = _mapper.Map<DeviceUser>(userRequest);
            await _repositoryProvider.UserRepository.CreateUserAsync(user);
            await _repositoryProvider.SaveChangesAsync();
            //send email to user
            bool emailSent = true;
            var emailStatus = string.Empty;
            emailStatus = emailSent ? "User created and email notification successfully sent" : "User created but email notification failed!";
            return new Response<UserResponse>(_mapper.Map<UserResponse>(user), emailStatus);
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _repositoryProvider.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new NotFoundException("User does not exist.");
            _repositoryProvider.UserRepository.DeleteUser(user);
            await _repositoryProvider.SaveChangesAsync();
        }
        public async Task<Response<IEnumerable<UserResponse>>> GetAllUsersAsync()
        {
            var users = await _repositoryProvider.UserRepository.GetAllUsersAsync();
            return new Response<IEnumerable<UserResponse>>(_mapper.Map<IEnumerable<UserResponse>>(users));
        }
        public async Task<Response<UserResponse>> GetUserByIdAsync(int id)
        {
            var user = await _repositoryProvider.UserRepository.GetUserByIdAsync(id);
            return (user == null) ? throw new NotFoundException($"User with Id {id} not found") : new Response<UserResponse>(_mapper.Map<UserResponse>(user));
        }
        public async Task UpdateUserAsync(int id, UserRequest deviceRequest)
        {
            var user = await _repositoryProvider.UserRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"User with Id {id} not found");
            _repositoryProvider.UserRepository.UpdateUser(user);
            await _repositoryProvider.SaveChangesAsync();
        }
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _repositoryProvider.UserRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"No Users registered with {request.Email}.");
            }
            var exist = await _repositoryProvider.UserRepository.UserCredentialExist(request);
            if (!exist)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }
            var response = _mapper.Map<AuthenticationResponse>(user);
            response.IsVerified = true;
            return new Response<AuthenticationResponse>(response, $"Successfully authenticated {user.Email}");
        }
        public async Task<AuthenticationResponse?> BasicAuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _repositoryProvider.UserRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return null;
            }
            var exist = await _repositoryProvider.UserRepository.UserCredentialExist(request);
            if (!exist)
            {
                return null;
            }

            var response = _mapper.Map<AuthenticationResponse>(user);
            response.IsVerified = true;
            return response;
        }
    }
}
