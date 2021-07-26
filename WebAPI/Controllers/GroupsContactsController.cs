using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsContactsController : ControllerBase
    {
        private IGroupContactService _groupContactService;

        public GroupsContactsController(IGroupContactService groupContactService)
        {
            _groupContactService = groupContactService;
        }
        [HttpPost(template: "Add")]
        public IActionResult Add(GroupContact groupContact)
        {
            var result = _groupContactService.Add(groupContact);
            if (result.Success) return Ok(result.Message);          
            else return BadRequest(result.Message);        
        }
        [HttpPost(template: "Delete")]
        public IActionResult Delete(GroupContact groupContact)
        {
            var result = _groupContactService.Delete(groupContact);
            if (result.Success) return Ok(result.Message);         
            else return BadRequest(result.Message);
        }
        [HttpGet(template: "GetListByContactId")]
        public IActionResult GetListByContactId(int contactId)
        {
            var result = _groupContactService.GetListByContactId(contactId);
            if (result.Success) return Ok(result.Data);       
            else return BadRequest(result.Message);
        }
        [HttpGet(template: "GetListByGroupId")]
        public IActionResult GetListByGroupId(int groupId)
        {
            var result = _groupContactService.GetListByGroupId(groupId);
            if (result.Success) return Ok(result.Data);  
            else return BadRequest(result.Message);
        }
        [HttpGet(template: "GetListByGroupIdPagination")]
        public IActionResult GetListByGroupIdPagination(int groupId, int pageNumber, int pageSize)
        {
            var result = _groupContactService.GetListByGroupId(groupId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);
        }
    }
}