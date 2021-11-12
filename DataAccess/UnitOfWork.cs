using TMDContactsApp.DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using TMDContactsApp.Core.Entities.Concrete;
using Microsoft.Extensions.Options;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private AnContactDal _contactDal;
        private AnGroupDal _groupDal;
        private AnGroupContactDal _groupcontactDal;
        private AnUserDal _userDal;
        private readonly IOptions<ConnectionSettings> _connectionSettings;
        public UnitOfWork(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public IContactDal Contacts => _contactDal ?? new AnContactDal(_connectionSettings);
        public IGroupDal Groups => _groupDal ?? new AnGroupDal(_connectionSettings);
        public IGroupContactDal GroupsContacts => _groupcontactDal ?? new AnGroupContactDal(_connectionSettings);
        public IUserDal Users => _userDal ?? new AnUserDal(_connectionSettings);
    }
}
