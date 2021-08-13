using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWebUI.Models.ViewComponents
{
    public class GroupListViewComponent: ViewComponent
    {
        IGroupService _groupService;

        public GroupListViewComponent(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public ViewViewComponentResult Invoke()
        {
            GroupListViewModel model = new GroupListViewModel { Groups = _groupService.GetList().Data };
            return View(model);
        }
    }
}