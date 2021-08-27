using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IContactService
    {
        Task<IDataResult<Contact>> GetById(int contactId);
        Task<IDataResult<List<Contact>>> GetList();
        Task<IDataResult<PaginationDto<Contact>>> GetListByUserId(int userId, int pageNumber, int pageSize);
        Task<IDataResult<List<Contact>>> GetListByUserId(int userId);
        Task<IDataResult<Contact>> IsUserInContactsList(int userId, string tel);
        Task<IResult> Add(Contact contact);
        Task<IResult> AddWithSynch(Contact contact);
        Task<IResult> Delete(int contact);
        IResult Delete(List<int> contacts);
        Task<IResult> Update(Contact contact);
    }
}
