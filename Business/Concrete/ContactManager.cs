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
        private IContactDal _contactDal;
 
        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public IResult Add(Contact contact)
        {
            _contactDal.Add(contact);
            return new SuccessResult(Messages.ContactAddSuccess);
        }

        public IResult Delete(Contact contact)
        {
            _contactDal.Delete(contact);
            return new SuccessResult(Messages.ContactDeleteSuccess);
        }

        public IResult Delete(List<Contact> contacts)
        { 
            foreach (var contact in contacts)
            {
                _contactDal.Delete(contact);
            }
            return new SuccessResult(Messages.ContactsDeleteSuccess);
        }

        public IDataResult<Contact> GetById(int contactId)
        {
            return new SuccessDataResult<Contact>(_contactDal.Get(filter: p=>p.Id == contactId));
        }

        public IDataResult<List<Contact>> GetList()
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetList().ToList());
        }
        public IDataResult<List<Contact>> GetList(int pageNumber, int pageSize)
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetList(pageNumber, pageSize).ToList());
        }

        public IDataResult<List<Contact>> GetListByUserId(int userId)
        {
            return new SuccessDataResult<List<Contact>>(_contactDal.GetList(filter: p=>p.UserId == userId).ToList());
        }

        public IResult Update(Contact contact)
        {
            _contactDal.Update(contact);
            return new SuccessResult(Messages.ContactUpdateSuccess);
        }
    }
}
