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
            _groupDal.Add(group);
            return new SuccessResult(Messages.GroupAddSuccess);
        }

        public IResult Delete(Group group)
        {
            _groupDal.Delete(group);
            return new SuccessResult(Messages.GroupDeleteSuccess);
        }
        public IResult Delete(List<Group> groups)
        {
            foreach (var group in groups)
            {
                _groupDal.Delete(group);
            }
            return new SuccessResult(Messages.GroupDeleteSuccess);
        }

        public IDataResult<Group> GetById(int groupId)
        {
            return new SuccessDataResult<Group>(_groupDal.Get(filter: p => p.Id == groupId));
        }

        public IDataResult<List<Group>> GetList()
        {
            return new SuccessDataResult<List<Group>>(_groupDal.GetList().ToList());
        }

        public IDataResult<List<Group>> GetList(int pageNumber, int pageSize)
        {
            return new SuccessDataResult<List<Group>>(_groupDal.GetList(pageNumber, pageSize).ToList());
        }

        public IDataResult<List<Group>> GetList(User user)
        {
            return new SuccessDataResult<List<Group>>(_groupDal.GetList(filter: p => p.UserId == user.Id).ToList());
        }

        public IResult Update(Group group)
        {
            _groupDal.Update(group);
            return new SuccessResult(Messages.GroupUpdateSuccess);
        }
    }
}
