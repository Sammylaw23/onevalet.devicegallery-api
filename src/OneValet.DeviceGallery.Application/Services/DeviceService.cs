using AutoMapper;
using Newtonsoft.Json;
using OneValet.DeviceGallery.Application.Common.Exceptions;
using OneValet.DeviceGallery.Application.DTOs.Device;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.Interfaces.Services;
using OneValet.DeviceGallery.Application.Wrappers;
using OneValet.DeviceGallery.Domain.Entities;
using OneValet.DeviceGallery.Domain.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Abstractions;
using OneValet.DeviceGallery.Application.ResourceParameters;

namespace OneValet.DeviceGallery.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepositoryProvider _repositoryProvider;
        private readonly IMapper _mapper;

        public DeviceService(IRepositoryProvider repositoryProvider, IMapper mapper)
        {
            _repositoryProvider = repositoryProvider;
            _mapper = mapper;
        }
        public async Task<Response<DeviceResponse>> AddDeviceAsync(DeviceRequest deviceRequest)
        {
            var deviceTypeExist = await _repositoryProvider.DeviceRepository.DeviceTypeExistAsync(deviceRequest.DeviceTypeId);
            if (!deviceTypeExist)
            {
                throw new ApiException($"'{deviceRequest.DeviceTypeId}' is not a valid device type");
            }
            var device = _mapper.Map<Domain.Entities.Device>(deviceRequest);
            await _repositoryProvider.DeviceRepository.CreateDeviceAsync(device);
            await _repositoryProvider.SaveChangesAsync();
            return new Response<DeviceResponse>(_mapper.Map<DeviceResponse>(device));
        }
        public async Task<Response<IEnumerable<DeviceResponse>>> GetAllDevicesAsync(DevicesResourceParameters devicesResourceParameters)
        {
            var devices = await _repositoryProvider.DeviceRepository.GetAllDeviceAsync(devicesResourceParameters);
            return new Response<IEnumerable<DeviceResponse>>(_mapper.Map<IEnumerable<DeviceResponse>>(devices));
        }
        public async Task<Response<DeviceResponse>> GetDeviceByIdAsync(int id)
        {
            var device = await _repositoryProvider.DeviceRepository.GetDeviceByIdAsync(id);
            return device == null ? throw new NotFoundException() : new Response<DeviceResponse>(_mapper.Map<DeviceResponse>(device));
        }
        public async Task UpdateDeviceAsync(int id, DeviceRequest deviceRequest)
        {
            var device = await _repositoryProvider.DeviceRepository.GetDeviceByIdAsync(id);
            if (device == null)
                throw new NotFoundException();
            _mapper.Map(deviceRequest, device);
            _repositoryProvider.DeviceRepository.UpdateDevice(device);
            await _repositoryProvider.SaveChangesAsync();

        }
        public async Task ToggleAvailability(int id)
        {
            var device = await _repositoryProvider.DeviceRepository.GetDeviceByIdAsync(id);
            if (device == null)
                throw new NotFoundException();
            device.IsOnline = (device.IsOnline) ? false : true;
            _repositoryProvider.DeviceRepository.UpdateDevice(device);
            await _repositoryProvider.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var device = await _repositoryProvider.DeviceRepository.GetDeviceByIdAsync(id);
            if (device == null)
                throw new NotFoundException("Device not found!");
            _repositoryProvider.DeviceRepository.DeleteDevice(device);
            await _repositoryProvider.SaveChangesAsync();
        }

        public async Task<Response<IEnumerable<DeviceResponse>>> AddMultipleDevicesAsync(IEnumerable<DeviceRequest> deviceRequests)
        {
            var devices = _mapper.Map<IEnumerable<Domain.Entities.Device>>(deviceRequests);
            await _repositoryProvider.DeviceRepository.AddMultipleDevicesAsync(devices);
            return new Response<IEnumerable<DeviceResponse>>(_mapper.Map<IEnumerable<DeviceResponse>>(devices));
        }
    }
}
