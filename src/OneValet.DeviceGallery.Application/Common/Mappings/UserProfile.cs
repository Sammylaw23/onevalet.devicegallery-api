using AutoMapper;
using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, DeviceUser>();
            CreateMap<DeviceUser, UserResponse>();
            CreateMap<DeviceUser, AuthenticationResponse>();
        }
    }
}
