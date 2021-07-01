using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IGroupContactService
    {
        IDataResult<List<Contact>> GetListByGroupId(int groupId);
        IDataResult<List<Group>> GetListByContactId(int contactId);
        IResult Add(GroupContact groupContact);
        IResult Delete(GroupContact groupContact);
    }
}
