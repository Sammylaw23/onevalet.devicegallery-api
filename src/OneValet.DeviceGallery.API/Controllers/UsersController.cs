using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneValet.DeviceGallery.Application.DTOs.User;
using OneValet.DeviceGallery.Application.Interfaces.Services;
using OneValet.DeviceGallery.API.Middlewares;
namespace OneValet.DeviceGallery.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers or Adds a user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUser(UserRequest user)
        {
            var response = await _userService.AddUserAsync(user);
            return Ok(response);
        }

        /// <summary>
        /// Authenticates users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authentication")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticateUser(AuthenticationRequest request)
        {
            return Ok(await _userService.AuthenticateAsync(request));
        }

        /// <summary>
        /// Returns a user for a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="404">If the item is not found with specified id</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        /// <summary>
        /// Returns all users in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        /// <summary>
        /// Deletes a user from the table in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
