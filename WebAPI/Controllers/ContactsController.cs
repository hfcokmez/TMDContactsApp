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
    public class ContactsController : ControllerBase
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet(template: "GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _contactService.GetList();
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);
        }
        [HttpGet(template: "GetListByUserId")]
        public IActionResult GetListByUserId(int userId)
        {
            var result = _contactService.GetListByUserId(userId);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);   
        }
        [HttpGet(template: "GetListByUserIdPagination")]
        public IActionResult GetListByUserIdPagination(int userId, int pageNumber, int pageSize)
        {
            var result = _contactService.GetListByUserId(userId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);
        }
        [HttpGet(template: "Get")]
        public IActionResult Get(int id)
        {
            var result = _contactService.GetById(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result.Message);       
        }
        [HttpPost(template: "Add")]
        public IActionResult Add(Contact contact)
        {
            var result = _contactService.Add(contact);
            if (result.Success) return Ok(result.Message);
            else return BadRequest(result.Message);     
        }
        [HttpPost(template: "AddWithSynch")]
        public IActionResult AddWithSynch(Contact contact)
        {
            var addWithSynch = _contactService.AddWithSynch(contact);
            if (addWithSynch.Success) return Ok(addWithSynch.Message);
            else return BadRequest(addWithSynch.Message);
        }
        [HttpPost(template: "Delete")]
        public IActionResult Delete(int id)
        {
            var result = _contactService.Delete(id);
            if (result.Success) return Ok(result.Message);
            else return BadRequest(result.Message);  
        }
        [HttpPost(template: "DeleteMultiple")]
        public IActionResult DeleteMultiple(List<int> contacts)
        {
            var result = _contactService.Delete(contacts);
            if (result.Success) return Ok(result.Message);
            else return BadRequest(result.Message);
        }
        [HttpPost(template: "Update")]
        public IActionResult Update(Contact contact)
        {
            var result = _contactService.Update(contact);
            if (result.Success) return Ok(result.Message);
            else return BadRequest(result.Message);       
        }
    }
}