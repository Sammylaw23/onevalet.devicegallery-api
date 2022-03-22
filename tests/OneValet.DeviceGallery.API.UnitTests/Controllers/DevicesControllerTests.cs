using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OneValet.DeviceGallery.API.Controllers;
using OneValet.DeviceGallery.API.UnitTests.MockData;
using OneValet.DeviceGallery.Application.DTOs.Device;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.Interfaces.Services;
using OneValet.DeviceGallery.Application.ResourceParameters;
using OneValet.DeviceGallery.Application.Services;
using OneValet.DeviceGallery.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OneValet.DeviceGallery.API.UnitTests.Controllers
{
    public class DevicesControllerTests
    {
        private readonly DevicesController _controller;
        private readonly Mock<IDeviceService> _service;
        public DevicesControllerTests()
        {
            _service = new Mock<IDeviceService>();
            _controller = new DevicesController(_service.Object);
        }


        [Fact]
        public async Task GetDevicesAsync_ShouldReturn200Status()
        {
            //Arrange
            var devicesResourceParameters = new DevicesResourceParameters()
            {
                Online = "",
                SearchQuery = ""
            };
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup(_ => _.GetAllDevicesAsync(devicesResourceParameters)).Returns(Task.FromResult(DeviceMockData.GetDevices()));
            var sut = new DevicesController(deviceService.Object);

            //Act
            var result = await sut.GetDevicesAsync(devicesResourceParameters);

            //Assert

            result.GetType().Should().Be(typeof(OkObjectResult));
            ((OkObjectResult)result).StatusCode.Should().Be(200);

        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturn200Status()
        {
            var result = await _controller.GetDeviceByIdAsync(11);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ShouldReturnExactNumberOfDevices()
        {
            //Arrange
            var devicesResourceParameters = new DevicesResourceParameters()
            {
                Online = "",
                SearchQuery = ""
            };
            _service.Setup(_ => _.GetAllDevicesAsync(devicesResourceParameters)).Returns(Task.FromResult(DeviceMockData.GetDevices()));

            //Act
            var result = await _controller.GetDevicesAsync(devicesResourceParameters);

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var devices = Assert.IsType<Response<IEnumerable<DeviceResponse>>>(viewResult.Value);
            Assert.Equal(2, devices.Data.Count());
        }

        [Fact]
        public async Task AddDeviceAsync_ShouldCallAddDeviceAsyncOnce()
        {
            //Arrange
            var newDevice = DeviceMockData.AddDevice();
            var newDeviceResponse = DeviceMockData.GetDevices().Data.FirstOrDefault();
            _service.Setup(x => x.AddDeviceAsync(newDevice)).Returns(Task.FromResult(new Response<DeviceResponse>(newDeviceResponse)));

            //Act
            var result = await _controller.AddDeviceAsync(newDevice);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            _service.Verify(_ => _.AddDeviceAsync(newDevice), Times.Once());
        }

        [Fact]
        public async Task GetDeviceByIdAsync_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 9;
            _service.Setup(_ => _.GetDeviceByIdAsync(testId)).Returns(Task.FromResult(new Response<DeviceResponse>(DeviceMockData.GetDevices().Data.First(x => x.Id == testId))));

            // Act
            var okResult = await _controller.GetDeviceByIdAsync(testId) as OkObjectResult;

            // Assert
            okResult.GetType().Should().Be(typeof(OkObjectResult));
            (okResult as OkObjectResult).StatusCode.Should().Be(200);

            Assert.IsType<Response<DeviceResponse>>(okResult.Value);
            var value = okResult.Value as Response<DeviceResponse>;
            Assert.Equal(testId, value.Data.Id);
        }
    }
}




