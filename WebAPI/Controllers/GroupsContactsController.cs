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
        [HttpPost(template: "Add")]
        public async Task<IActionResult> Add(GroupContact groupContact)
        {
            var result = await _groupContactService.Add(groupContact);
            if (result.Success) return Ok(result);          
            else return BadRequest(result);        
        }
        [HttpPost(template: "Delete")]
        public IActionResult Delete(GroupContact groupContact)
        {
            var result = _groupContactService.Delete(groupContact);
            if (result.Success) return Ok(result);         
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByContactId")]
        public async Task<IActionResult> GetListByContactId(int contactId)
        {
            var result = await _groupContactService.GetListByContactId(contactId);
            if (result.Success) return Ok(result.Data);       
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByGroupId")]
        public async Task<IActionResult> GetListByGroupId(int groupId)
        {
            var result = await _groupContactService.GetListByGroupId(groupId);
            if (result.Success) return Ok(result.Data);  
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByGroupIdPagination")]
        public async Task<IActionResult> GetListByGroupIdPagination(int groupId, int pageNumber, int pageSize)
        {
            var result = await _groupContactService.GetListByGroupId(groupId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
    }
}