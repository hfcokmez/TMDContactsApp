using Core.DataAccess.AdoNet;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

namespace DataAccess.Concrete
{
    public class AnContactDal: AnEntityRepositoryBaseAsync<Contact>, IContactDal
    {
    }
}
