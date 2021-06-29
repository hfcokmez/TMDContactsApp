using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfGroupContactDal: EfEntityRepositoryBase<GroupContact, TMDContactsAppContext>, IGroupContactDal
    {
        public List<Contact> GetContactsByGroupId(int groupId)
        {
            using (var context = new TMDContactsAppContext())
            {
                var result = from groupContact in context.Groups_Contacts
                             join contact in context.Contacts
                             on groupContact.ContactId equals contact.Id
                             where groupContact.GroupId == groupId
                             select new Contact { Id = contact.Id, Name = contact.Name,
                             Surname = contact.Surname, Tel = contact.Tel, TelBusiness = contact.TelBusiness,
                             TelHome = contact.TelHome, Email = contact.Email, Address = contact.Address,
                             Photo = contact.Photo, Company = contact.Company, Title = contact.Title,
                             BirthDate = contact.BirthDate, Note = contact.Note, UserId = contact.UserId};
                return result.ToList();
            }
        }
    }
}
