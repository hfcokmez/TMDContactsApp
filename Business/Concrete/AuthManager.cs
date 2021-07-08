using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Services;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using Core.Utilities.Security;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JsonWebToken;
using Entities.Dto;
using System;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IEmailSendHelper _emailSendHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IEmailSendHelper emailSendHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _emailSendHelper = emailSendHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            //var userClaims = _userService.GetUserOperationClaims(user);
            var accessToken = _tokenHelper.CreateToken(user);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            var userCheck = _userService.GetByEmail(userLoginDto.Email);
            if (userCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userCheck.Data.PasswordHash, userCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }
            return new SuccessDataResult<User>(userCheck.Data, Messages.SuccessLogin);
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                Email = userRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
                Status = true,
                Tel = userRegisterDto.Tel,
                TelBusiness = userRegisterDto.TelBusiness,
                TelHome = userRegisterDto.TelHome,
                Address = userRegisterDto.Address,
                Photo = userRegisterDto.Photo,
                Company = userRegisterDto.Company,
                Title = userRegisterDto.Title,
                BirthDate = userRegisterDto.BirthDate,
                Note = userRegisterDto.Note,
            };
            var result = _userService.Add(user);
            if (result.Success)
            {
                return new SuccessDataResult<User>(user, Messages.UserRegistered);
            }
            else return new ErrorDataResult<User>(user, Messages.UserAddFail);

        }
        public IDataResult<int> Verification(string email)
        {
            int randomNumber = new Random().Next(1000, 10000);
            //silinecek
            return new SuccessDataResult<int>(1453);

            if (_emailSendHelper.SendEmailWithContext(email, "<h1>Hello World!</h1>", randomNumber.ToString()))
            {
                return new SuccessDataResult<int>(randomNumber);
            }
            return new ErrorDataResult<int>();
        }
        public IDataResult<User> ResetPassword(User user, UserLoginDto userLoginDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userLoginDto.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Update(user);

            return new SuccessDataResult<User>(user, Messages.ResetSuccess);
        }
        public IDataResult<User> ResetPassword(User user, UserLoginDto userLoginDto, string currentPassword)
        {
            if (HashingHelper.VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt))
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(userLoginDto.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                var result = _userService.Update(user);
                if (result.Success) return new SuccessDataResult<User>(user, Messages.ResetSuccess);
                else return new ErrorDataResult<User>(Messages.UserUpdateFail);
            }
            else return new ErrorDataResult<User>(Messages.PasswordsNotMatched);
        }
        public IDataResult<User> UserExists(string email)
        {
            var userExists = _userService.GetByEmail(email);
            if (userExists == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(userExists.Data, Messages.UserAlreadyExists);
        }
    }
}
