using Microsoft.EntityFrameworkCore;
using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Application.Interfaces;
using OneValet.DeviceGallery.Application.Interfaces.Repositories;
using OneValet.DeviceGallery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneValet.DeviceGallery.Infrastructure.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<DeviceUser>, IUserRepository
    {
        public UserRepository(IApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task CreateUserAsync(DeviceUser user)
            => await AddAsync(user);
        public void DeleteUser(DeviceUser user)
            => Delete(user);
        public async Task<IEnumerable<DeviceUser>> GetAllUsersAsync()
            => await Get().ToListAsync();
        public async Task<DeviceUser> GetUserByEmailAsync(string email)
            => await _dbContext.DeviceUsers.FirstOrDefaultAsync(x => x.Email == email);
        public async Task<DeviceUser> GetUserByIdAsync(int id)
    => await GetByIdAsync(id);
        public async Task<DeviceUser> GetUserByUserNameAsync(string userName)
    => await _dbContext.DeviceUsers.FirstOrDefaultAsync(x => x.UserName == userName);
        public void UpdateUser(DeviceUser user)
            => Update(user);

        public async Task<bool> UserCredentialExist(AuthenticationRequest user)
        {
            var dbUser = await _dbContext.DeviceUsers.FirstOrDefaultAsync(x=>x.Password == user.Password);
            //var dbUser = await _dbContext.DeviceUsers.FirstOrDefaultAsync(x=> x.Email == user.Email && x.Password == user.Password);
            return (dbUser != null && dbUser.Password == user.Password) ? true : false;
        }
    }
}
