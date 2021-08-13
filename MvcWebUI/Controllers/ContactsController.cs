using Business.Abstract;
using Core.Utilities.Results;
using MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebUI.Controllers
{
    public class ContactsController : Controller
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
            var result = new SuccessResult();
        }
        // GET: Contact
        public ActionResult Index()
        {
            ContactListViewModel model = new ContactListViewModel { Contacts = _contactService.GetList().Data };
            return View(model);
        }
    }
}