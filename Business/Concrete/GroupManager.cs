using Business.Abstract;
using Core.Entities.Concrete;
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
    public class GroupManager: IGroupService
    {
        private EGroupDal _groupDal;
        public GroupManager(EGroupDal groupDal)
        {
            _groupDal = groupDal;
        }

        public IResult Add(Group group)
        {
            if (_groupDal.Add(group, "AddGroup"))
            {
                return new SuccessResult(Messages.GroupAddSuccess);
            }
            return new ErrorResult(Messages.GroupAddFail);
        }

        public IResult Delete(Group group)
        {
            if (_groupDal.Delete(group, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
        }
        public IResult Delete(List<Group> groups)
        {
            foreach (var group in groups)
            {
                if (_groupDal.Delete(group, "DeleteGroup"))
                {
                    return new SuccessResult(Messages.GroupDeleteSuccess);
                }
                return new ErrorResult(Messages.GroupDeleteFail);
            }
            return new SuccessResult(Messages.GroupDeleteSuccess);
        }

        public IDataResult<Group> GetById(int groupId)
        {
            var group = _groupDal.Get(groupId, "GetGroup");
            if (group != null)
            {
                return new SuccessDataResult<Group>(group);
            }
            return new ErrorDataResult<Group>(Messages.GroupGetFail);
        }

        public IDataResult<List<Group>> GetList()
        {
            return new SuccessDataResult<List<Group>>(_groupDal.GetList("GetAllGroups").ToList());
        }

        public IDataResult<List<Group>> GetList(int pageNumber, int pageSize)
        {
            return null;
        }

        public IDataResult<List<Group>> GetList(int userId)
        {
            var groupList = _groupDal.GetList(userId, "UserId", "GetGroupsByUserId").ToList();
            if (groupList != null)
            {
                return new SuccessDataResult<List<Group>>(groupList);
            }
            return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
        }

        public IResult Update(Group group)
        {
            if (_groupDal.Update(group, "UpdateGroup"))
            {
                return new SuccessResult(Messages.GroupUpdateSuccess);
            }
            return new ErrorResult(Messages.GroupUpdateFail);
        }
    }
}
