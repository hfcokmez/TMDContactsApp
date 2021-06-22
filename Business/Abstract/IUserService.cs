using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetList();
        IDataResult<User> GetById(int userId);
        IDataResult<User> GetByEmail(string email);
        List<OperationClaim> GetUserOperationClaims(User user);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
    }
}
