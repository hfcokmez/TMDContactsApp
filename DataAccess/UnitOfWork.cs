using TMDContactsApp.DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private AnContactDal _contactDal;
        private AnGroupDal _groupDal;
        private AnGroupContactDal _groupcontactDal;
        private AnUserDal _userDal;


        public IContactDal Contacts => _contactDal ?? new AnContactDal();
        public IGroupDal Groups => _groupDal ?? new AnGroupDal();
        public IGroupContactDal GroupsContacts => _groupcontactDal ?? new AnGroupContactDal();
        public IUserDal Users => _userDal ?? new AnUserDal();
    }
}
