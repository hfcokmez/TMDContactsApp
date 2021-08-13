using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Services;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet(template: "GetAll")]
        public IActionResult GetAll()
        {
            var result = _contactService.GetList();
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByUserId")]
        public IActionResult GetListByUserId(int userId)
        {
            var result = _contactService.GetListByUserId(userId);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);   
        }
        [HttpGet(template: "GetListByUserIdPagination")]
        public IActionResult GetListByUserIdPagination(int userId, int pageNumber, int pageSize)
        {
            var result = _contactService.GetListByUserId(userId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
        [HttpGet(template: "Get")]
        public IActionResult Get(int id)
        {
            var result = _contactService.GetById(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);       
        }
        [HttpPost (template: "Add")]
        public IActionResult Add(Contact contact)
        {
            var result = _contactService.Add(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);     
        }
        [HttpPost(template: "AddWithSynch")]
        public IActionResult AddWithSynch(Contact contact)
        {
            var result = _contactService.AddWithSynch(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "Delete")]
        public IActionResult Delete(int id)
        {
            var result = _contactService.Delete(id);
            if (result.Success) return Ok(result);
            else return BadRequest(result);  
        }
        [HttpPost(template: "DeleteMultiple")]
        public IActionResult DeleteMultiple(List<int> contacts)
        {
            var result = _contactService.Delete(contacts);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "Update")]
        public IActionResult Update(Contact contact)
        {
            var result = _contactService.Update(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);       
        }
    }
}