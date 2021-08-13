using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MvcWeb.Models;

namespace MvcWeb.Controllers
{
    public class GroupController : Controller
    {
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public IActionResult Index()
        {
            GroupListViewModel model = new GroupListViewModel { Groups = _groupService.GetList().Data };
            return View(model);
        }
    }
}