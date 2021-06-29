using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IGroupService
    {
        IDataResult<Group> GetById(int groupId);
        IDataResult<List<Group>> GetList();
        IDataResult<List<Group>> GetList(int Id);
        IDataResult<List<Group>> GetList(int pageNumber, int pageSize);
        IResult Add(Group group);
        IResult Delete(Group group);
        IResult Delete(List<Group> groups);
        IResult Update(Group group);
    }
}
