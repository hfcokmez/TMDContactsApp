using Core.DataAccess.AdoNet;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Concrete
{
    public class AnGroupContactDal: AnEntityRepositoryBaseAsync<GroupContact>, IGroupContactDal
    {
    }
}
