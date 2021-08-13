using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using Core.Utilities.Services;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        private IContactDal _contactDal;
        private IGroupDal _groupDal;
        private IUserService _userService;
        public ContactManager(IContactDal contactDal, IGroupDal groupDal, IUserService userService)
        {
            _contactDal = contactDal;
            _groupDal = groupDal;
            _userService = userService;
        }

        public IResult Add(Contact contact)
        {
            if (IsUserInContactsList(contact.UserId, contact.Tel).Success) return new ErrorResult(Messages.ContactAlreadyExist);
            if (_contactDal.Add(contact, "AddContact"))
            {
                return new SuccessResult(Messages.ContactAddSuccess);
            }
            return new ErrorResult(Messages.ContactAddFail);
        }

        public IResult Delete(int contact)
        {
            if (_contactDal.Delete(contact, "DeleteContact"))
            {
                return new SuccessResult(Messages.ContactDeleteSuccess);
            }
            return new ErrorResult(Messages.ContactDeleteFail);
        }

        public IResult Delete(List<int> contacts)
        {
            if (_contactDal.Delete(contacts, "DeleteContact"))
            {
                return new SuccessResult(Messages.ContactsDeleteSuccess);
            }
            return new ErrorResult(Messages.ContactDeleteFail);
        }

        public IDataResult<Contact> GetById(int contactId)
        {
            var contact = _contactDal.Get(contactId, "GetContact");
            if (contact != null)
            {
                return new SuccessDataResult<Contact>(contact);
            }
            return new ErrorDataResult<Contact>(Messages.ContactGetFail);
        }

        public IDataResult<List<Contact>> GetList()
        {
            try
            {
                var contactList = _contactDal.GetList("GetAllContacts").ToList();
                return new SuccessDataResult<List<Contact>>(contactList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.ContactGetListFail);
            }
        }

        public IDataResult<PaginationDto<Contact>> GetListByUserId(int userId, int pageNumber, int pageSize)
        {
            var contactCount = _contactDal.GetCount(new { UserId = userId }, "Count", "GetContactCountByUserId");
            if (contactCount > 0)
            {
                var contactList = _contactDal.GetList(new { UserId = userId, PageNumber = pageNumber, PageSize = pageSize },
                    "GetContactsByUserIdPagination").ToList();
                var result = new PaginationDto<Contact>
                {
                    Data = contactList,
                    CurrentPage = pageNumber,
                    LastPage = contactCount / pageSize,
                    PageSize = pageSize,
                    TotalCount = contactCount
                };
                if (contactCount % pageSize > 0) result.LastPage = (contactCount / pageSize) + 1;
                else result.LastPage = contactCount / pageSize;
                if (pageNumber > 1) result.PreviousPage = pageNumber - 1;
                if (pageNumber < result.LastPage) result.NextPage = pageNumber + 1;
                return new SuccessDataResult<PaginationDto<Contact>>(result);
            }
                return new ErrorDataResult<PaginationDto<Contact>>(Messages.ContactGetListFail);
        }

        public IDataResult<List<Contact>> GetListByUserId(int userId)
        {
            try
            {
                var contactList = _contactDal.GetList(new { @UserId = userId }, "GetContactsByUserId").ToList();
                return new SuccessDataResult<List<Contact>>(contactList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.ContactGetListFail);
            }
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

        public IResult Update(Contact contact)
        {
            if (_contactDal.Update(contact, "UpdateContact"))
            {
                return new SuccessResult(Messages.ContactUpdateSuccess);
            }
            return new ErrorResult(Messages.ContactUpdateFail);
        }

        public IDataResult<Contact> IsUserInContactsList(int userId, string tel)
        {
            var contact = _contactDal.Get(new { @Id = userId, @Tel = tel }, "IsUserInContactsList");
            if (contact != null)
            {
                return new SuccessDataResult<Contact>(contact);
            }
            return new ErrorDataResult<Contact>();
        }

        public IResult AddWithSynch(Contact contact)
        {
            var userCheck = _userService.GetByTel(contact.Tel);
            if (userCheck.Success)
            {
                var tel = _userService.GetById(contact.UserId).Data.Tel;
                var userInContactList = IsUserInContactsList(userCheck.Data.Id, tel);
                if (userInContactList.Success)
                {
                    var contactExist = ObjectBindHelper.ObjectToObject<User, Contact>(userCheck.Data, contact);
                    var resultExist = Add(contactExist);
                    if (resultExist.Success) return new SuccessResult(resultExist.Message);
                    else return new ErrorResult(resultExist.Message);
                }
            }
            var result = Add(contact);
            if (result.Success) return new SuccessResult(result.Message);
            else return new ErrorResult(result.Message);
        }
    }
}
