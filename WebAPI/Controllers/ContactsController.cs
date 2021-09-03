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
        [HttpGet(template: "GetAllAsync")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactService.GetListAsync();
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
        [HttpGet(template: "GetListByUserIdAsync")]
        public async Task<IActionResult> GetListByUserId(int userId)
        {
            var result = await _contactService.GetListByUserIdAsync(userId);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);   
        }
        [HttpGet(template: "GetListByUserIdPaginationAsync")]
        public async Task<IActionResult> GetListByUserIdPaginationAsync(int userId, int pageNumber, int pageSize)
        {
            var result = await _contactService.GetListByUserIdAsync(userId, pageNumber, pageSize);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);
        }
        [HttpGet(template: "GetAsync")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _contactService.GetByIdAsync(id);
            if (result.Success) return Ok(result.Data);
            else return BadRequest(result);       
        }
        [HttpPost (template: "AddAsync")]
        public async Task<IActionResult> Add(Contact contact)
        {
            var result = await _contactService.AddAsync(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);     
        }
        [HttpPost(template: "AddWithAsync")]
        public async Task<IActionResult> AddWithAsynch(Contact contact)
        {
            var result = await _contactService.AddWithAsync(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _contactService.DeleteAsync(id);
            if (result.Success) return Ok(result);
            else return BadRequest(result);  
        }
        [HttpPost(template: "DeleteMultipleAsync")]
        public async Task<IActionResult> DeleteListAsync(List<int> contacts)
        {
            var result = await _contactService.DeleteListAsync(contacts);
            if (result.Success) return Ok(result);
            else return BadRequest(result);
        }
        [HttpPost(template: "UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(Contact contact)
        {
            var result = await _contactService.UpdateAsync(contact);
            if (result.Success) return Ok(result);
            else return BadRequest(result);       
        }
    }
}