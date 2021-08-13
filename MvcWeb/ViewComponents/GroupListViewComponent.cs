using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MvcWeb.Models;

namespace MvcWeb.ViewComponents
{
    public class GroupListViewComponent : ViewComponent
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
