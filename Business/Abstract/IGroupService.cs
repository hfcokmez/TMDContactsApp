using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupService
    {
        Task<IDataResult<Group>> GetById(int groupId);
        Task<IDataResult<List<Group>>> GetList();
        Task<IDataResult<List<Group>>> GetList(int userId);
        Task<IResult> Add(Group group);
        Task<IResult> Delete(int group);
        IResult Delete(List<int> groups);
        Task<IResult> Update(Group group);
    }
}
