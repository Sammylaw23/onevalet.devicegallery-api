using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using OneValet.DeviceGallery.API.UnitTests.MockData;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.ResourceParameters;
using OneValet.DeviceGallery.Application.Services;
using OneValet.DeviceGallery.Domain.Entities;
using OneValet.DeviceGallery.Infrastructure.Contexts;
using OneValet.DeviceGallery.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OneValet.DeviceGallery.API.UnitTests.Services
{
    public class DeviceServiceTest : IDisposable
    {
        private readonly DeviceDbContext _dbContext;
        private IRepositoryProvider _repository;
        public DeviceServiceTest()
        {
            var options = new DbContextOptionsBuilder<DeviceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new DeviceDbContext(options);
            _dbContext.Database.EnsureCreated();
            _repository = new RepositoryProvider(_dbContext);
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
