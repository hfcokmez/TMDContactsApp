﻿using System;
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
    [Route("[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private IAuthService _authService;
        public AuthsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost(template: "Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userLogin = await _authService.LoginAsync(userLoginDto);
            if (!userLogin.Success)return BadRequest(userLogin);
            var result = await _authService.CreateAccessToken(userLogin.Data);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var userRegister = await _authService.RegisterAsync(userRegisterDto);
            if (!userRegister.Success)return BadRequest(userRegister);
            var result = await _authService.CreateAccessToken(userRegister.Data);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "RefreshToken")]
        public async Task<IActionResult> RefreshToken(User user)
        {
            var result = await _authService.CreateAccessToken(user);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpGet(template: "Verification")]
        public async Task<IActionResult> Verification(string email)
        {
            var result = await _authService.VerificationAsync(email);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _authService.VerificationAsync(email);
            if (result.Success)return Ok(result.Data);
            return BadRequest(result);
        }
        [HttpPost(template: "ResetPassword")]
        public async Task<IActionResult> ResetPassword(UserLoginDto userLoginDto)
        {
            var result = await _authService.ResetPasswordAsync(userLoginDto);
            if (!result.Success)return BadRequest(result);
            return Ok(result);
        }
        [HttpPost(template: "ResetPasswordVerification")]
        [Authorize]
        public async Task<IActionResult> ResetPasswordVerification(UserLoginDto userLoginDto, string currentPassword)
        {
            var result = await _authService.ResetPasswordAsync(userLoginDto, currentPassword);
            if (!result.Success)return BadRequest(result);
            return Ok(result);
        }
    }
}