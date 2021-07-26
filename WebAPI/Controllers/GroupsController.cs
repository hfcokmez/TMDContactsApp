using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpPost(template: "Add")]
        public IActionResult Add(Group group)
        {
            var result = _groupService.Add(group);
            if (result.Success) return Ok(result.Message); 
            else return BadRequest(result.Message);      
        }
        [HttpPost(template: "Delete")]
        public IActionResult Delete(int id)
        {
            var result = _groupService.Delete(id);
            if (result.Success) return Ok(result.Message);        
            else return BadRequest(result.Message);
        }
        [HttpPost(template: "DeleteMultiple")]
        public IActionResult DeleteMultiple(List<int> groups)
        {
            var result = _groupService.Delete(groups);
            if (result.Success) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPost(template: "Update")]
        public IActionResult Update(Group group)
        {
            var result = _groupService.Update(group);
            if (result.Success) return Ok(result.Message);           
            else return BadRequest(result.Message);       
        }
        [HttpGet(template: "Get")]
        public IActionResult Get(int id)
        {
            var result = _groupService.GetById(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);     
        }
        [HttpGet(template: "GetAll")]
        public IActionResult GetList()
        {
            var result = _groupService.GetList();
            if (result.Success) return Ok(result.Data);       
            else return BadRequest(result.Message);
        }
        [HttpGet(template: "GetListByUserId")]
        public IActionResult GetUserGroups(int userId)
        {
            var result = _groupService.GetList(userId);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);    
        }
    }
}