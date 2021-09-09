using Core;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TMDContactsApp.DataAccess.Abstract
{
    public interface IContactDal: IEntityRepositoryAsync<Contact>
    {
    }
}
