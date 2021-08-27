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
        Task<IDataResult<User>> Register(UserRegisterDto userRegisterDto);
        Task<IDataResult<User>> Login(UserLoginDto userLoginDto);
        Task<IDataResult<User>> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task<IResult> ResetPassword(UserLoginDto userLoginDto);
        Task<IResult> ResetPassword(UserLoginDto userLoginDto, string currentPassword);
        Task<IDataResult<int>> Verification(string email);
    }
}
