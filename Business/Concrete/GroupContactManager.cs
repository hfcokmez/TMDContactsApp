using Business.Abstract;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using TMDContactsApp.DataAccess.Abstract;
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

        public async Task<IResult> AddAsync(GroupContact groupContact)
        {
            var dto = new { GroupId = groupContact.GroupId, ContactId = groupContact.ContactId };
            if (await _unitOfWork.GroupsContacts.GetListAsync(dto, "GetGroupContact") != null) return new ErrorResult(Messages.ContactAlreadyExistInGroup);
            if (await _unitOfWork.GroupsContacts.AddAsync(groupContact, "AddGroupContact"))
            {
                return new SuccessResult(Messages.GroupContactAddSuccess);
            }
            return new ErrorResult(Messages.GroupContactAddFail);
        }

        public async Task<IResult> DeleteAsync(GroupContact groupContact)
        {
            if (await _unitOfWork.GroupsContacts.DeleteAsync(new { @GroupId = groupContact.GroupId, @ContactId = groupContact.ContactId }, "DeleteGroupContact"))
            {
                return new SuccessResult(Messages.GroupContactDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupContactDeleteFail);
        }

        public async Task<IDataResult<List<Group>>> GetListByContactIdAsync(int contactId)
        {
            try
            {
                var contactDto = new { ContactId = contactId };
                var contactGroupList = await _unitOfWork.Groups.GetListAsync(contactDto, "GetGroupsOfAContact");
                return new SuccessDataResult<List<Group>>(contactGroupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GetGroupsOfAContactFail);
            }
        }

        public async Task<IDataResult<List<Contact>>> GetListByGroupIdAsync(int groupId)
        {
            try
            {
                var group = new { GroupId = groupId };
                var groupContactList = await _unitOfWork.Contacts.GetListAsync(group, "GetContactsOfAGroup");
                return new SuccessDataResult<List<Contact>>(groupContactList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }

        public async Task<IDataResult<List<Contact>>> GetListByGroupIdAsync(int groupId, int pageNumber, int pageSize)
        {
            var groupDto = new { GroupId = groupId, PageNumber = pageNumber, PageSize = pageSize};
            try
            {
                var groupContactList = await _unitOfWork.Contacts.GetListAsync(groupDto, "GetContactsOfAGroupPagination");
                return new SuccessDataResult<List<Contact>>(groupContactList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.GetContactsOfAGroupFail);
            }
        }
    }
}
