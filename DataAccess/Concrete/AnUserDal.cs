using Core.DataAccess.AdoNet;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class AnUserDal: AnEntityRepositoryBaseAsync<User>, IUserDal
    {
    }
}
