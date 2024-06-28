
using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Exceptions;
using H2H.Physiotherapy.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Controllers
{
    public class MobileController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public MobileController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("GenerateOtp/{username}")]
        public async Task<IActionResult> GenerateOtp(string username)
        {
            ApiResponse response = await _loginService.GenerateOtp(username);
            response.Complete();
            return Ok(response);
        }

        [HttpPost("ValidateOtp")]
        public async Task<IActionResult> ValidateOtp([FromBody]OtpDTO otpDTO)
        {
            ApiResponse response = await _loginService.ValidateOtp(otpDTO);
            response.Complete();
            return Ok(response);
        }

        [HttpPost]
        [Route("AccessTokenFromRefreshToken")]
        public async Task<IActionResult> AccessTokenFromRefreshToken([FromBody]RefreshTokenPost refreshtoken)
        {
            try
            {
                var response = await _loginService.GenerateAccessTokenFromRefreshToken(refreshtoken);
                return Ok(response);
            }
            catch (DataNotFoundException ex)
            {
                return BadRequest(new { error = "invalid_grant", error_description = ex.Message });
            }
        }

    }
}
