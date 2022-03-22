using AutoMapper;
using OneValet.DeviceGallery.Application.DTOs.Device;
using OneValet.DeviceGallery.Domain.Entities;
using OneValet.DeviceGallery.Domain.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Common.Mappings
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceResponse>();
            CreateMap<DeviceRequest, Device>();
        }
    }
}
