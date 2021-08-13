using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IContactService
    {
        IDataResult<Contact> GetById(int contactId);
        IDataResult<List<Contact>> GetList();
        IDataResult<PaginationDto<Contact>> GetListByUserId(int userId, int pageNumber, int pageSize);
        IDataResult<List<Contact>> GetListByUserId(int userId);
        IDataResult<Contact> IsUserInContactsList(int userId, string tel);
        IResult Add(Contact contact);
        IResult AddWithSynch(Contact contact);
        IResult Delete(int contact);
        IResult Delete(List<int> contacts);
        IResult Update(Contact contact);
    }
}
