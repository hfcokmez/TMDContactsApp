using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupContactService
    {
        Task<IDataResult<List<Contact>>> GetListByGroupIdAsync(int groupId);
        Task<IDataResult<List<Group>>> GetListByContactIdAsync(int contactId);
        Task<IDataResult<List<Contact>>> GetListByGroupIdAsync(int groupId, int pageNumber, int pageSize);
        Task<IResult> AddAsync(GroupContact groupContact);
        Task<IResult> DeleteAsync(GroupContact groupContact);
    }
}
