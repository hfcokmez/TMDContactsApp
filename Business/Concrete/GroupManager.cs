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
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GroupManager: IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GroupManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(Group group)
        {
            if (await _unitOfWork.Groups.Get(new { @UserId = group.UserId, @Name = group.Name }, "IsGroupInUsersList") != null) return new ErrorResult(Messages.GroupAlreadyExists);
            if (await _unitOfWork.Groups.Add(group, "AddGroup"))
            {
                return new SuccessResult(Messages.GroupAddSuccess);
            }
            return new ErrorResult(Messages.GroupAddFail);
        }

        public async Task<IResult> Delete(int group)
        {
            if (await _unitOfWork.Groups.Delete(group, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
        }

        public IResult Delete(List<int> groups)
        {
            if (_unitOfWork.Groups.Delete(groups, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupsDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
        }

        public async Task<IDataResult<Group>> GetById(int groupId)
        {
            var group = await _unitOfWork.Groups.Get(new { @Id = groupId }, "GetGroup");
            if (group != null)
            {
                return new SuccessDataResult<Group>(group);
            }
            return new ErrorDataResult<Group>(Messages.GroupGetFail);
        }

        public async Task<IDataResult<List<Group>>> GetList()
        {
            try
            {
                var groupList = await _unitOfWork.Groups.GetList("GetAllGroups");
                return new SuccessDataResult<List<Group>>(groupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
            }
        }

        public async Task<IDataResult<List<Group>>> GetList(int userId)
        {
            try
            {
                var user = new { UserId = userId };
                var groupList = await _unitOfWork.Groups.GetList(user, "GetGroupsByUserId");
                return new SuccessDataResult<List<Group>>(groupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
            }
        }

        public async Task<IResult> Update(Group group)
        {
            if (await _unitOfWork.Groups.Update(group, "UpdateGroup"))
            {
                return new SuccessResult(Messages.GroupUpdateSuccess);
            }
            return new ErrorResult(Messages.GroupUpdateFail);
        }
    }
}
