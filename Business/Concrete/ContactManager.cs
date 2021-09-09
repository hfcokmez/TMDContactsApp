using AutoMapper;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using Core.Utilities.Services;
using DataAccess;
using TMDContactsApp.DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        public ContactManager(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<IResult> AddAsync(Contact contact)
        {
            var isUserInContactsList = await IsUserInContactsListAsync(contact.UserId, contact.Tel);
            if (isUserInContactsList.Success) return new ErrorResult(Messages.ContactAlreadyExist);
            if (await _unitOfWork.Contacts.AddAsync(contact, "AddContact"))
            {
                return new SuccessResult(Messages.ContactAddSuccess);
            }
            return new ErrorResult(Messages.ContactAddFail);
        }

        public async Task<IResult> DeleteAsync(int contact)
        {
            if (await _unitOfWork.Contacts.DeleteAsync(new { @Id = contact}, "DeleteContact"))
            {
                return new SuccessResult(Messages.ContactDeleteSuccess);
            }
            return new ErrorResult(Messages.ContactDeleteFail);
        }

        public async Task<IResult> DeleteListAsync(List<int> contacts)
        {
            if (await _unitOfWork.Contacts.DeleteListAsync(contacts, "DeleteContact"))
            {
                return new SuccessResult(Messages.ContactsDeleteSuccess);
            }
            return new ErrorResult(Messages.ContactDeleteFail);
        }
        public async Task<IDataResult<Contact>> GetByIdAsync(int contactId)
        {

            var contact = await _unitOfWork.Contacts.GetAsync(new { @Id = contactId }, "GetContact");
            if (contact != null)
            {
                return new SuccessDataResult<Contact>(contact);
            }
            return new ErrorDataResult<Contact>(Messages.ContactGetFail);
        }

        public async Task<IDataResult<List<Contact>>> GetListAsync()
        {
            try
            {
                var contactList = await _unitOfWork.Contacts.GetListAsync("GetAllContacts");
                return new SuccessDataResult<List<Contact>>(contactList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.ContactGetListFail);
            }
        }

        public async Task<IDataResult<PaginationDto<Contact>>> GetListByUserIdAsync(int userId, int pageNumber, int pageSize)
        {
            var contactCount = await _unitOfWork.Contacts.GetCountAsync(new { UserId = userId }, "Count", "GetContactCountByUserId");
            if (contactCount > 0)
            {
                var contactList = await _unitOfWork.Contacts.GetListAsync(new { UserId = userId, PageNumber = pageNumber, PageSize = pageSize },
                    "GetContactsByUserIdPagination");
                var result = new PaginationDto<Contact>
                {
                    Data = contactList.ToList(),
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

        public async Task<IDataResult<List<Contact>>> GetListByUserIdAsync(int userId)
        {
            try
            {
                var contactList = await _unitOfWork.Contacts.GetListAsync(new { @UserId = userId }, "GetContactsByUserId");
                return new SuccessDataResult<List<Contact>>(contactList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Contact>>(Messages.ContactGetListFail);
            }
        }

        public async Task<IDataResult<List<Group>>> GetListByContactIdAsync(int contactId)
        {
            try
            {
                var contact = new { ContactId = contactId };
                var contactGroupList = await _unitOfWork.Groups.GetListAsync(contact, "GetGroupsOfAContact");
                return new SuccessDataResult<List<Group>>(contactGroupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GetGroupsOfAContactFail);
            }
        }

        public async Task<IResult> UpdateAsync(Contact contact)
        {
            if (await _unitOfWork.Contacts.UpdateAsync(contact, "UpdateContact"))
            {
                return new SuccessResult(Messages.ContactUpdateSuccess);
            }
            return new ErrorResult(Messages.ContactUpdateFail);
        }

        public async Task<IDataResult<Contact>> IsUserInContactsListAsync(int userId, string tel)
        {
            var contact = await _unitOfWork.Contacts.GetAsync(new { @Id = userId, @Tel = tel }, "IsUserInContactsList");
            if (contact != null)
            {
                return new SuccessDataResult<Contact>(contact);
            }
            return new ErrorDataResult<Contact>();
        }

        public async Task<IResult> AddWithAsync(Contact contact)
        {
            var userCheck = await _userService.GetByTelAsync(contact.Tel);
            if (userCheck.Success)
            {
                var task = await _userService.GetByIdAsync(contact.UserId);
                var tel = task.Data.Tel;
                var userInContactList = await IsUserInContactsListAsync(userCheck.Data.Id, tel);
                if (userInContactList.Success)
                {
                    var contactExist = ObjectBindHelper.ObjectToObject<User, Contact>(userCheck.Data, contact);
                    var resultExist = await AddAsync(contactExist);
                    if (resultExist.Success) return new SuccessResult(resultExist.Message);
                    else return new ErrorResult(resultExist.Message);
                }
            }
            var result = await AddAsync(contact);
            if (result.Success) return new SuccessResult(result.Message);
            else return new ErrorResult(result.Message);
        }
    }
}
