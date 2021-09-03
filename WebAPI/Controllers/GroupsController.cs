using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost(template: "AddAsync")]
        public async Task<IActionResult> AddAsync(Group group)
        {
            var result = await _groupService.AddAsync(group);
            if (result.Success) return Ok(result); 
            else return BadRequest(result);      
        }
        [HttpPost(template: "DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _groupService.DeleteAsync(id);
            if (result.Success) return Ok(result);        
            else return BadRequest(result);
        }
        [HttpPost(template: "DeleteMultipleAsync")]
        public async Task<IActionResult> DeleteListAsync(List<int> groups)
        {
            var result = await _groupService.DeleteListAsync(groups);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Group group)
        {
            var result = await _groupService.UpdateAsync(group);
            if (result.Success) return Ok(result);           
            else return BadRequest(result);       
        }
        [HttpGet(template: "GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _groupService.GetByIdAsync(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);     
        }
        [HttpGet(template: "GetAllAsync")]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await _groupService.GetListAsync();
            if (result.Success) return Ok(result.Data);       
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByUserIdAsync")]
        public async Task<IActionResult> GetUserGroupsAsync(int userId)
        {
            var result = await _groupService.GetListAsync(userId);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);    
        }
    }
}