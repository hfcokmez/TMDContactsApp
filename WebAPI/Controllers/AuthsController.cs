using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Contents;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
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
            if (!userLogin.Success)return BadRequest(userLogin);
            var result = _authService.CreateAccessToken(userLogin.Data);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "Register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userRegister = _authService.Register(userRegisterDto);
            if (!userRegister.Success)return BadRequest(userRegister);
            var result = _authService.CreateAccessToken(userRegister.Data);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "RefreshToken")]
        public IActionResult RefreshToken(User user)
        {
            var result = _authService.CreateAccessToken(user);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpGet(template: "Verification")]
        public IActionResult Verification(string email)
        {
            var result = _authService.Verification(email);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var result = _authService.Verification(email);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "ResetPassword")]
        public IActionResult ResetPassword(UserLoginDto userLoginDto)
        {
            var result = _authService.ResetPassword(userLoginDto);
            if (!result.Success)return BadRequest(result);
            return Ok(result);
        }
        [HttpPost(template: "ResetPasswordVerification")]
        [Authorize]
        public IActionResult ResetPasswordVerification(UserLoginDto userLoginDto, string currentPassword)
        {
            var result = _authService.ResetPassword(userLoginDto, currentPassword);
            if (!result.Success)return BadRequest(result);
            return Ok(result);
        }
    }
}