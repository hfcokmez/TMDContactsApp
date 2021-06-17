using Core.DataAccess.AdoNet;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.AdoNet
{
    public class AnGroupDal: AnEntityRepositoryBase<Group, TMDContactsAppContext>, IGroupDal
    {
    }
}
