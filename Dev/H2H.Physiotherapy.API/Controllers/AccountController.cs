using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Controllers
{
    [Authorize]
    public class AccountController : ControllerBase
    {
       
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            ApiResponse response = await _userService.GetAllUsers();
            response.Complete();
            return Ok(response);
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            ApiResponse response = await _userService.GetUserById(userId);
            response.Complete();
            return Ok(response);
        }

        [HttpDelete("InActivateUser/{userId}")]
        public async Task<IActionResult> InactivateUser([FromRoute] string userId)
        {
            var response = await _userService.InActivateUser(userId);
            response.Complete();
            return Ok(response);
        }

        [HttpPost("UpsertUser")]
        public async Task<IActionResult> UpsertUser([FromBody] UserAddDTO userAddDTO)
        {
            var response = await _userService.UpsertUser(userAddDTO);
            response.Complete();
            return Ok(response);
        }
    }
}
