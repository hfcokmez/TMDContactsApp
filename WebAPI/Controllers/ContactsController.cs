using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Services;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet(template: "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetList();
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByUserId")]
        public async Task<IActionResult> GetListByUserId(int userId)
        {
            var result = await _contactService.GetListByUserId(userId);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);   
        }
        [HttpGet(template: "GetListByUserIdPagination")]
        public async Task<IActionResult> GetListByUserIdPagination(int userId, int pageNumber, int pageSize)
        {
            var result = await _contactService.GetListByUserId(userId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
        [HttpGet(template: "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contactService.GetById(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);       
        }
        [HttpPost (template: "Add")]
        public async Task<IActionResult> Add(Contact contact)
        {
            var result = await _contactService.Add(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);     
        }
        [HttpPost(template: "AddWithSynch")]
        public async Task<IActionResult> AddWithSynch(Contact contact)
        {
            var result = await _contactService.AddWithSynch(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _contactService.Delete(id);
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
        public async Task<IActionResult> Update(Contact contact)
        {
            var result = await _contactService.Update(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);       
        }
    }
}