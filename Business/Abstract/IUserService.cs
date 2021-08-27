using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IDataResult<List<User>>> GetList();
        Task<IDataResult<User>> GetById(int userId);
        Task<IDataResult<User>> GetByEmail(string email);
        Task<IDataResult<User>> GetByTel(string tel);
        List<OperationClaim> GetUserOperationClaims(User user);
        Task<IResult> Add(User user);
        Task<IResult> Delete(int user);
        IResult Delete(List<int> users);
        Task<IResult> Update(User user);
    }
}
