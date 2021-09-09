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
        Task<IDataResult<List<User>>> GetListAsync();
        Task<IDataResult<User>> GetByIdAsync(int userId);
        Task<IDataResult<User>> GetByEmailAsync(string email);
        Task<IDataResult<User>> GetByTelAsync(string tel);
        Task<List<OperationClaim>> GetUserOperationClaims(User user);
        Task<IResult> AddAsync(User user);
        Task<IResult> DeleteAsync(int user);
        Task<IResult> DeleteListAsync(List<int> users);
        Task<IResult> UpdateAsync(User user);
    }
}
