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
        Task<IDataResult<List<Contact>>> GetListByGroupId(int groupId);
        Task<IDataResult<List<Group>>> GetListByContactId(int contactId);
        Task<IDataResult<List<Contact>>> GetListByGroupId(int groupId, int pageNumber, int pageSize);
        Task<IResult> Add(GroupContact groupContact);
        IResult Delete(GroupContact groupContact);
    }
}
