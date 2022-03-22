using OneValet.DeviceGallery.Application.DTOs.Device;
using OneValet.DeviceGallery.Application.ResourceParameters;
using OneValet.DeviceGallery.Application.Wrappers;
using OneValet.DeviceGallery.Domain.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Application.Interfaces.Services
{
    public interface IDeviceService
    {
        Task<Response<DeviceResponse>> AddDeviceAsync(DeviceRequest deviceRequest);
        Task<Response<IEnumerable<DeviceResponse>>> AddMultipleDevicesAsync(IEnumerable<DeviceRequest> deviceRequests);
        Task UpdateDeviceAsync(int id, DeviceRequest deviceRequest);
        Task<Response<DeviceResponse>> GetDeviceByIdAsync(int id);
        Task<Response<IEnumerable<DeviceResponse>>> GetAllDevicesAsync(DevicesResourceParameters devicesResourceParameters);
        Task ToggleAvailability(int id);
        Task DeleteDeviceAsync(int id);

    }
}