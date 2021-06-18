using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;

        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost(template: "Login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var userLogin = _authService.Login(userLoginDto);
            if (!userLogin.Success)
            {
                return BadRequest(userLogin.Success);
            }
            var result = _authService.CreateAccessToken(userLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost(template: "Register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userExists = _authService.userExists(userRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var userRegister = _authService.Register(userRegisterDto);
            if (!userRegister.Success)
            {
                return BadRequest(userExists.Message);
            }
            var result = _authService.CreateAccessToken(userRegister.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}