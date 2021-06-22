using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Contents;
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
            var userExists = _authService.UserExists(userRegisterDto.Email);
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
        [HttpPost(template: "Verification")]
        public IActionResult Verification(string email)
        {
            //Send the user an email:
            var mailResult = _authService.Verification(email);
            if (mailResult.Success)
            {
                return Ok(mailResult.Data);
            }
            return BadRequest();
        }
        [HttpPost(template: "ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var userExists = _authService.UserExists(email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            //Send the user an email:
            var mailResult = _authService.Verification(email);
            if (mailResult.Success)
            {
                return Ok(mailResult.Data);
            }
            return BadRequest();
        }
        [HttpPost(template: "ResetPassword")]
        public IActionResult ResetPassword(UserLoginDto userLoginDto)
        {
            var userExists = _authService.UserExists(userLoginDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }
            var passwordReset = _authService.ResetPassword(userExists.Data, userLoginDto);
            if (!passwordReset.Success)
            {
                return BadRequest(passwordReset.Message);
            }
            return Ok(passwordReset.Message);
        }
        
    }
}