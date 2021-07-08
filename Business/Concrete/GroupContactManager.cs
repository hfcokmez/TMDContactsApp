using Business.Abstract;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class GroupContactManager: IGroupContactService
    {
        private EGroupContactDal _groupContactDal;
        private EGroupDal _groupDal;
        private EContactDal _contactDal;
        public GroupContactManager(EGroupContactDal groupContactDal, EGroupDal groupDal, EContactDal contactDal)
        {
            _groupContactDal = groupContactDal;
            _groupDal = groupDal;
            _contactDal = contactDal;
        }

        public IResult Add(GroupContact groupContact)
        {
            if (_groupContactDal.Add(groupContact, "AddGroupContact"))
            {
                return new SuccessResult(Messages.GroupContactAddSuccess);
            }
            return new ErrorResult(Messages.GroupContactAddFail);
        }

        public IResult Delete(GroupContact groupContact)
        {
            if (_groupContactDal.Delete(groupContact, "DeleteGroupContact"))
            {
                return new SuccessResult(Messages.GroupContactDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupContactDeleteFail);
        }

        public IDataResult<List<Group>> GetListByContactId(int contactId)
        {
            try
            {
                var contactGroupList = _groupDal.GetList(contactId, "ContactId", "GetGroupsOfAContact").ToList();
                return new SuccessDataResult<List<Group>>(contactGroupList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GetGroupsOfAContactFail);
            }
        }

        public IDataResult<List<Contact>> GetListByGroupId(int groupId)
        {
            try
            {
                var groupContactList = _contactDal.GetList(groupId, "GroupId", "GetContactsOfAGroup").ToList();
                return new SuccessDataResult<List<Contact>>(groupContactList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }
    }
}
