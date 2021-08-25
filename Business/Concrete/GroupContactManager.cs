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
        private readonly IUnitOfWork _unitOfWork;
        public GroupContactManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IResult Add(GroupContact groupContact)
        {
            var dto = new { GroupId = groupContact.GroupId, ContactId = groupContact.ContactId };
            if (_unitOfWork.GroupsContacts.GetList(dto, "GetGroupContact") != null) return new ErrorResult(Messages.ContactAlreadyExistInGroup);
            if (_unitOfWork.GroupsContacts.Add(groupContact, "AddGroupContact"))
            {
                return new SuccessResult(Messages.GroupContactAddSuccess);
            }
            return new ErrorResult(Messages.GroupContactAddFail);
        }

        public IResult Delete(GroupContact groupContact)
        {
            if (_unitOfWork.GroupsContacts.Delete(groupContact, "DeleteGroupContact"))
            {
                return new SuccessResult(Messages.GroupContactDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupContactDeleteFail);
        }

        public IDataResult<List<Group>> GetListByContactId(int contactId)
        {
            try
            {
                var contactGroupList = _unitOfWork.Groups.GetList(contactId, "ContactId", "GetGroupsOfAContact").ToList();
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
                var groupContactList = _unitOfWork.Contacts.GetList(groupId, "GroupId", "GetContactsOfAGroup").ToList();
                return new SuccessDataResult<List<Contact>>(groupContactList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }

        public IDataResult<List<Contact>> GetListByGroupId(int groupId, int pageNumber, int pageSize)
        {
            var groupDto = new { GroupId = groupId, PageNumber = pageNumber, PageSize = pageSize};
            try
            {
                var groupContactList = _unitOfWork.Contacts.GetList(groupDto, "GetContactsOfAGroupPagination").ToList();
                return new SuccessDataResult<List<Contact>>(groupContactList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }
    }
}
