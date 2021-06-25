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
    public class ContactManager: IContactService
    {
        private EContactDal _contactDal;
 
        public ContactManager(EContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public IResult Add(Contact contact)
        {
            if (_contactDal.Add(contact, "AddContact"))
            {
                return new SuccessResult(Messages.ContactAddSuccess);
            }
            return new ErrorResult(Messages.ContactAddFail);
        }

        public IResult Delete(Contact contact)
        {
            if (_contactDal.Delete(contact,"DeleteContact"))
            {
                return new SuccessResult(Messages.ContactDeleteSuccess);
            }
            return new ErrorResult(Messages.ContactDeleteFail);
        }

        public IResult Delete(List<Contact> contacts)
        { 
            foreach (var contact in contacts)
            {
                _contactDal.Delete(contact,"DeleteContact");
            }
            return new SuccessResult(Messages.ContactsDeleteSuccess);
        }

        public IDataResult<Contact> GetById(int contactId)
        {
            Contact contact = _contactDal.Get(contactId,"GetContact");
            if(contact != null)
            {
                return new SuccessDataResult<Contact>(contact);
            }
            return new ErrorDataResult<Contact>(Messages.ContactGetFail);
            
        }

        public IDataResult<List<Contact>> GetList()
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetList("").ToList());
        }
        public IDataResult<List<Contact>> GetList(int pageNumber, int pageSize)
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetList(pageNumber, pageSize, "").ToList());
        }

        public IDataResult<List<Contact>> GetListByUserId(int userId)
        {
            var contactList = _contactDal.GetList(userId,"GetContactsByUserId").ToList();
            if(contactList != null)
            {
                return new SuccessDataResult<List<Contact>>(contactList);
            }
            return new ErrorDataResult<List<Contact>>(Messages.ContactGetListFail);
        }

        public IResult Update(Contact contact)
        {
            if (_contactDal.Update(contact,"UpdateContact"))
            {
                return new SuccessResult(Messages.ContactUpdateSuccess);
            }
            return new ErrorResult(Messages.ContactUpdateFail);
        }
    }
}
