using Business.Abstract;
using Business.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<GroupManager>().As<IGroupService>();
            builder.RegisterType<GroupContactManager>().As<IGroupContactService>();

            builder.RegisterType<AnContactDal>().As<IContactDal>();
            builder.RegisterType<AnGroupDal>().As<IGroupDal>();
            builder.RegisterType<AnUserDal>().As<IUserDal>();
            builder.RegisterType<AnGroupContactDal>().As<IGroupContactDal>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
