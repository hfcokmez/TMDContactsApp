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
        private IGroupDal _groupDal;
        public GroupManager(IGroupDal groupDal)
        {
            _groupDal = groupDal;
        }

        public IResult Add(Group group)
        {
            var dto = new { UserId = group.UserId, Name = group.Name };
            if (_groupDal.Get(dto, "IsGroupInUsersList") != null) return new ErrorResult(Messages.GroupAlreadyExists);
            if (_groupDal.Add(group, "AddGroup"))
            {
                return new SuccessResult(Messages.GroupAddSuccess);
            }
            return new ErrorResult(Messages.GroupAddFail);
        }

        public IResult Delete(int group)
        {
            if (_groupDal.Delete(group, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
        }

        public IResult Delete(List<int> groups)
        {
            if (_groupDal.Delete(groups, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupsDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
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
            try
            {
                var groupList = _groupDal.GetList("GetAllGroups").ToList();
                return new SuccessDataResult<List<Group>>(groupList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
            }
        }

        public IDataResult<List<Group>> GetList(int userId)
        {
            try
            {
                var groupList = _groupDal.GetList(userId, "UserId", "GetGroupsByUserId").ToList();
                return new SuccessDataResult<List<Group>>(groupList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
            }
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
