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
    public class ContactsController : ControllerBase
    {
        private IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet(template:"GetAll")]
        public IActionResult GetAll()
        {
            var result = _contactService.GetList();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "GetListByUserId")]
        public IActionResult GetListByUserId(int userId)
        {
            var result = _contactService.GetListByUserId(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpGet(template: "Get")]
        public IActionResult Get(int Id)
        {
            var result = _contactService.GetById(Id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "Add")]
        public IActionResult Add(Contact contact)
        {
            var result = _contactService.Add(contact);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "Delete")]
        public IActionResult Delete(Contact contact)
        {
            var result = _contactService.Delete(contact);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
        [HttpPost(template: "Update")]
        public IActionResult Update(Contact contact)
        {
            var result = _contactService.Update(contact);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}