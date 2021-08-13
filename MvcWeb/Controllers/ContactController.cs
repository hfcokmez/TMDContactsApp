using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MvcWeb.Models;

namespace MvcWeb.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            ContactListViewModel model = new ContactListViewModel { Contacts = _contactService.GetList().Data };
            return View(model);
        }
    }
}