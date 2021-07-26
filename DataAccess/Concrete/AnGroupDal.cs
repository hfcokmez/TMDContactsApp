using Core.DataAccess.AdoNet;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class AnGroupDal : AnEntityRepositoryBase<Group>, IGroupDal
    {
        public Group Get(Group group, string procedure)
        {
            throw new NotImplementedException();
        }
    }
}
