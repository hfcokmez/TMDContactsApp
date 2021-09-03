using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsContactsController : ControllerBase
    {
        private IGroupContactService _groupContactService;
        public GroupsContactsController(IGroupContactService groupContactService)
        {
            _groupContactService = groupContactService;
        }
        [HttpPost(template: "AddAsync")]
        public async Task<IActionResult> AddAsync(GroupContact groupContact)
        {
            var result = await _groupContactService.AddAsync(groupContact);
            if (result.Success) return Ok(result);          
            else return BadRequest(result);        
        }
        [HttpPost(template: "DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(GroupContact groupContact)
        {
            var result = await _groupContactService.DeleteAsync(groupContact);
            if (result.Success) return Ok(result);         
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByContactIdAsync")]
        public async Task<IActionResult> GetListByContactIdAsync(int contactId)
        {
            var result = await _groupContactService.GetListByContactIdAsync(contactId);
            if (result.Success) return Ok(result.Data);       
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByGroupIdAsync")]
        public async Task<IActionResult> GetListByGroupIdAsync(int groupId)
        {
            var result = await _groupContactService.GetListByGroupIdAsync(groupId);
            if (result.Success) return Ok(result.Data);  
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByGroupIdPaginationAsync")]
        public async Task<IActionResult> GetListByGroupIdPaginationAsync(int groupId, int pageNumber, int pageSize)
        {
            var result = await _groupContactService.GetListByGroupIdAsync(groupId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
    }
}