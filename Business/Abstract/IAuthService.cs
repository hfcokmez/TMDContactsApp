using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserRegisterDto userRegisterDto);
        IDataResult<User> Login(UserLoginDto userLoginDto);
        IDataResult<User> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<User> ResetPassword(User user, UserLoginDto userLoginDto);
        IDataResult<User> ResetPassword(User user, UserLoginDto userLoginDto, string currentPassword);
        IDataResult<int> Verification(string email);
    }
}
