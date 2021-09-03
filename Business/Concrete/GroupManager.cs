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

        public async Task<IResult> AddAsync(Group group)
        {
            if (await _unitOfWork.Groups.GetAsync(new { @UserId = group.UserId, @Name = group.Name }, "IsGroupInUsersList") != null) return new ErrorResult(Messages.GroupAlreadyExists);
            if (await _unitOfWork.Groups.AddAsync(group, "AddGroup"))
            {
                return new SuccessResult(Messages.GroupAddSuccess);
            }
            return new ErrorResult(Messages.GroupAddFail);
        }

        public async Task<IResult> DeleteAsync(int group)
        {
            if (await _unitOfWork.Groups.DeleteAsync(new { @Id = group }, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
        }

        public async Task<IResult> DeleteListAsync(List<int> groups)
        {
            if (await _unitOfWork.Groups.DeleteListAsync(groups, "DeleteGroup"))
            {
                return new SuccessResult(Messages.GroupsDeleteSuccess);
            }
            return new ErrorResult(Messages.GroupDeleteFail);
        }

        public async Task<IDataResult<Group>> GetByIdAsync(int groupId)
        {
            var group = await _unitOfWork.Groups.GetAsync(new { @Id = groupId }, "GetGroup");
            if (group != null)
            {
                return new SuccessDataResult<Group>(group);
            }
            return new ErrorDataResult<Group>(Messages.GroupGetFail);
        }

        public async Task<IDataResult<List<Group>>> GetListAsync()
        {
            try
            {
                var groupList = await _unitOfWork.Groups.GetListAsync("GetAllGroups");
                return new SuccessDataResult<List<Group>>(groupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
            }
        }

        public async Task<IDataResult<List<Group>>> GetListAsync(int userId)
        {
            try
            {
                var user = new { UserId = userId };
                var groupList = await _unitOfWork.Groups.GetListAsync(user, "GetGroupsByUserId");
                return new SuccessDataResult<List<Group>>(groupList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<Group>>(Messages.GroupGetFail);
            }
        }

        public async Task<IResult> UpdateAsync(Group group)
        {
            if (await _unitOfWork.Groups.UpdateAsync(group, "UpdateGroup"))
            {
                return new SuccessResult(Messages.GroupUpdateSuccess);
            }
            return new ErrorResult(Messages.GroupUpdateFail);
        }
    }
}
