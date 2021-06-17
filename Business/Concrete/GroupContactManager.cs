using Business.Abstract;
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
    public class GroupContactManager: IGroupContactService
    {
        private IGroupContactDal _groupContactDal;

        public GroupContactManager(IGroupContactDal groupContactDal)
        {
            _groupContactDal = groupContactDal;
        }

        public IResult Add(GroupContact groupContact)
        {
            _groupContactDal.Add(groupContact);
            return new SuccessResult(Messages.GroupContactAddSuccess);
        }

        public IResult Delete(GroupContact groupContact)
        {
            _groupContactDal.Delete(groupContact);
            return new SuccessResult(Messages.GroupContactDeleteSuccess);
        }

        public IDataResult<List<GroupContact>> GetListByContactId(int contactId)
        {
            return new SuccessDataResult<List<GroupContact>>(_groupContactDal.GetList(filter: p => p.ContactId == contactId).ToList());
        }

        public IDataResult<List<GroupContact>> GetListByGroupId(int groupId)
        {
            return new SuccessDataResult<List<GroupContact>>(_groupContactDal.GetList(filter: p => p.GroupId == groupId).ToList());
        }
    }
}
