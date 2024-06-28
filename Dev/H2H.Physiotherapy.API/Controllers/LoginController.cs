using H2H.Physiotherapy.Common.Models;
using H2H.Physiotherapy.Common.Models.Account;
using H2H.Physiotherapy.Common.Models.DTO;
using H2H.Physiotherapy.Services.Abstractions.IServices;
using H2H.Physiotherapy.Services.Abstractions.OtherServices;
using H2H.Physiotherapy.Services.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IEmailService _emailService;
        public LoginController(ILoginService loginService, IRequestContext context)
        {
            _loginService = loginService;

        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Login([FromForm] UserCredential userCredential)
        {
            ApiResponse response = await _loginService.Login(userCredential);
            response.Complete();
            return Ok(response);

        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPassDto dto)
        {
            ApiResponse response = await _loginService.ForgetPassword(dto);
            response.Complete();
            return Ok(response);

        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPassDto dto)
        {
            ApiResponse response = await _loginService.ResetPassword(dto);
            response.Complete();
            return Ok(response);
        }

    }
}
