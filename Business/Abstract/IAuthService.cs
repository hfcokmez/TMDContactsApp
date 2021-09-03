using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<IDataResult<User>> LoginAsync(UserLoginDto userLoginDto);
        Task<IDataResult<User>> UserExistsAsync(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task<IResult> ResetPasswordAsync(UserLoginDto userLoginDto);
        Task<IResult> ResetPasswordAsync(UserLoginDto userLoginDto, string currentPassword);
        Task<IDataResult<int>> VerificationAsync(string email);
    }
}
