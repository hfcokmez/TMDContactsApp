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
        [HttpPost(template: "Add")]
        public async Task<IActionResult> Add(User user)
        {
            var result = await _userService.Add(user);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "Update")]
        public async Task<IActionResult> Update(User user)
        {
            var result = await _userService.Update(user);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpGet(template: "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userService.GetById(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result); 
        }
        [HttpGet(template: "GetByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _userService.GetByEmail(email);
            if (result.Success) return Ok(result.Data);    
            else return BadRequest(result);
        }
        [HttpGet(template: "GetAll")]
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetList();
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);         
        }
    }
}