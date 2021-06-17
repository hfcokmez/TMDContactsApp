using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.JsonWebToken;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolver
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactManager>().As<IContactService>();
            builder.RegisterType<EfContactDal>().As<IContactDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<GroupManager>().As<IGroupService>();
            builder.RegisterType<EfGroupDal>().As<IGroupDal>();

            builder.RegisterType<GroupContactManager>().As<IGroupContactService>();
            builder.RegisterType<EfGroupContactDal>().As<IGroupContactDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            
        }
    }
}
