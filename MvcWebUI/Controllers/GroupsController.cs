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
    public class GroupsController : Controller
    {
        private IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        //public ActionResult Index()
        //{
        //    GroupListViewModel model = new GroupListViewModel { Groups = _groupService.GetList().Data };
        //    return View(model);
        //}
    }
}