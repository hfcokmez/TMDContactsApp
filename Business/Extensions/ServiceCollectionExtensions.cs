using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.JsonWebToken;
using Core.Utilities.Security.Jwt;
using DataAccess;
using DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TMDContactsApp.DataAccess.Abstract;

namespace TMDContactsApp.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IContactService, ContactManager>();
            services.AddScoped<IContactDal, AnContactDal>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, AnUserDal>();

            services.AddScoped<IGroupService, GroupManager>();
            services.AddScoped<IGroupDal, AnGroupDal>();

            services.AddScoped<IGroupContactService, GroupContactManager>();
            services.AddScoped<IGroupContactDal, AnGroupContactDal>();

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenHelper, JwtHelper>();

            return services;
        }
    }
}
