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
        private readonly IEmailSendHelper _emailSendHelper;
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
            var userExists = UserExists(userLoginDto.Email);
            if (userExists.Data == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userLoginDto.Password, userExists.Data.PasswordHash, userExists.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }
            return new SuccessDataResult<User>(userExists.Data, Messages.SuccessLogin);
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            var emailExists = UserExists(userRegisterDto.Email);
            var telExist = _userService.GetByTel(userRegisterDto.Tel);
            if (emailExists.Success) return new ErrorDataResult<User>(emailExists.Message);
            else if (telExist.Success) return new ErrorDataResult<User>(telExist.Message); 
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
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
            if (result.Success) return new SuccessDataResult<User>(user, Messages.UserRegistered);
            else return new ErrorDataResult<User>(Messages.UserAddFail);

        }

        public IDataResult<int> Verification(string email)
        {
            var userExists = UserExists(email);
            if (!userExists.Success)
            {
                return new ErrorDataResult<int>(userExists.Message);
            }
            int randomNumber = new Random().Next(1000, 10000);
            //silinecek
            return new SuccessDataResult<int>(randomNumber);

            //if (_emailSendHelper.SendEmailWithContext(email, "<h1>Hello World!</h1>", randomNumber.ToString()))
            //{
            //    return new SuccessDataResult<int>(randomNumber);
            //}
            //return new ErrorDataResult<int>();
        }

        public IDataResult<User> ResetPassword(UserLoginDto userLoginDto)
        {
            var user = UserExists(userLoginDto.Email);
            if (!user.Success)
            {
                return new ErrorDataResult<User>(user.Message);
            }
            if (Login(userLoginDto).Success) return new ErrorDataResult<User>(Messages.SamePasswordFail);

            HashingHelper.CreatePasswordHash(userLoginDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Data.PasswordHash = passwordHash;
            user.Data.PasswordSalt = passwordSalt;
            _userService.Update(user.Data);

            return new SuccessDataResult<User>(user.Data, Messages.ResetSuccess);
        }

        public IDataResult<User> ResetPassword(UserLoginDto userLoginDto, string currentPassword)
        {
            UserLoginDto test = new UserLoginDto { Email = userLoginDto.Email, Password = currentPassword };
            var user = Login(test);
            if (!user.Success) return new ErrorDataResult<User>(user.Message);
            if (Login(userLoginDto).Success) return new ErrorDataResult<User>(Messages.SamePasswordFail);

            if (HashingHelper.VerifyPasswordHash(currentPassword, user.Data.PasswordHash, user.Data.PasswordSalt))
            {
                HashingHelper.CreatePasswordHash(userLoginDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Data.PasswordHash = passwordHash;
                user.Data.PasswordSalt = passwordSalt;
                var result = _userService.Update(user.Data);
                if (result.Success) return new SuccessDataResult<User>(user.Data, Messages.ResetSuccess);
                else return new ErrorDataResult<User>(Messages.UserUpdateFail);
            }
            else return new ErrorDataResult<User>(Messages.PasswordsNotMatched);
        }
        public IDataResult<User> UserExists(string email)
        {
            var userExists = _userService.GetByEmail(email);
            if (userExists.Data == null)
            {
                return new ErrorDataResult<User>( Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(userExists.Data, Messages.UserAlreadyExists);
        }
    }
}
