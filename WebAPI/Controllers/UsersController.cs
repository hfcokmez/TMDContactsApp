using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost(template: "AddAsync")]
        public async Task<IActionResult> AddAsync(User user)
        {
            var result = await _userService.AddAsync(user);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            var result = await _userService.UpdateAsync(user);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet(template: "GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result); 
        }
        [HttpGet(template: "GetByEmailAsync")]
        public async Task<IActionResult> GetByEmailAsync(string email)
        {
            var result = await _userService.GetByEmailAsync(email);
            if (result.Success) return Ok(result.Data);    
            else return BadRequest(result);
        }
        [HttpGet(template: "GetAllAsync")]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _userService.GetListAsync();
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);         
        }
    }
}