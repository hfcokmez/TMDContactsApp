using Business.Abstract;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GroupContactManager: IGroupContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupContactManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(GroupContact groupContact)
        {
            var dto = new { GroupId = groupContact.GroupId, ContactId = groupContact.ContactId };
            if (await _unitOfWork.GroupsContacts.GetList(dto, "GetGroupContact") != null) return new ErrorResult(Messages.ContactAlreadyExistInGroup);
            if (await _unitOfWork.GroupsContacts.Add(groupContact, "AddGroupContact"))
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

        public async Task<IDataResult<List<Group>>> GetListByContactId(int contactId)
        {
            try
            {
                var contactDto = new { ContactId = contactId };
                var contactGroupList = await _unitOfWork.Groups.GetList(contactDto, "GetGroupsOfAContact");
                return new SuccessDataResult<List<Group>>(contactGroupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GetGroupsOfAContactFail);
            }
        }

        public async Task<IDataResult<List<Contact>>> GetListByGroupId(int groupId)
        {
            try
            {
                var group = new { GroupId = groupId };
                var groupContactList = await _unitOfWork.Contacts.GetList(group, "GetContactsOfAGroup");
                return new SuccessDataResult<List<Contact>>(groupContactList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }

        public async Task<IDataResult<List<Contact>>> GetListByGroupId(int groupId, int pageNumber, int pageSize)
        {
            var groupDto = new { GroupId = groupId, PageNumber = pageNumber, PageSize = pageSize};
            try
            {
                var groupContactList = await _unitOfWork.Contacts.GetList(groupDto, "GetContactsOfAGroupPagination");
                return new SuccessDataResult<List<Contact>>(groupContactList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }
    }
}
