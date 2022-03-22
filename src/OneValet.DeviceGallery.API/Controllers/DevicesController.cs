using OneValet.DeviceGallery.Application.DTOs.Device;
using OneValet.DeviceGallery.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using OneValet.DeviceGallery.Domain.Entities.RequestFeatures;
using Newtonsoft.Json;
using OneValet.DeviceGallery.Application.ResourceParameters;

namespace OneValet.DeviceGallery.API.Controllers
{
    [Route("api/devices")]
    [ApiController]
    [Authorize]

    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Returns all devices in the database's table. Returned collection would be filtered if a value is passed to the Online property and the database would be searched if a value is passed 
        /// to the SearchQuery parameter.
        /// </summary>
        /// <param name="devicesResourceParameters"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDevicesAsync([FromQuery] DevicesResourceParameters devicesResourceParameters)
        {
            var devices = await _deviceService.GetAllDevicesAsync(devicesResourceParameters);
            return Ok(devices);
        }

        /// <summary>
        /// Returns a device for a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If the item is not found with specified id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeviceByIdAsync(int id) =>
           Ok(await _deviceService.GetDeviceByIdAsync(id));

        /// <summary>
        /// Adds a device to the database
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddDeviceAsync(DeviceRequest request)
        {
            var response = await _deviceService.AddDeviceAsync(request);
            return Ok(response);
            //CreatedAtAction(nameof(GetDeviceByIdAsync), new { id = response.Data }, response);
        }

        /// <summary>
        /// Adds multiple devices to the database with a single API call
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMultipleDevicesAsync(IEnumerable<DeviceRequest> requests)
        {
            var response = await _deviceService.AddMultipleDevicesAsync(requests);
            return Ok(response);
        }

        /// <summary>
        /// Updates a device by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDeviceAsync(int id, DeviceRequest request)
        {
            await _deviceService.UpdateDeviceAsync(id, request);
            return NoContent();
        }

        /// <summary>
        /// Deletes a device by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDeviceAsync(int id)
        {
            await _deviceService.DeleteDeviceAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Toggles availability of a device. To either change a device from Offline to Online or from online to Offline
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("toggledeviceavailability/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleDeviceAvailabilityAsync(int id)
        {
            await _deviceService.ToggleAvailability(id);
            return NoContent();
        }

    }
}