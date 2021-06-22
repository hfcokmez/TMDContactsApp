using Business.Abstract;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using Core.Entities.Concrete;
using Core.Utilities.Security;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JsonWebToken;
using DataAccess.Abstract;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Business.Concrete
{
    public class AuthManager: IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper; 
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
            if(userCheck == null)
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
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        public IDataResult<int> Verification(string email)
        {
            int randomNumber = new Random().Next(1000, 10000);
            //Email operations:
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("TMD-ContactsApp", "brainwest400@gmail.com");
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("User", email);
            message.To.Add(to);
            message.Subject = "Şifre yenileme bilgilendirmesi.";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
            bodyBuilder.TextBody = randomNumber.ToString();
            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("brainwest400@gmail.com", "samsungmonte");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return new SuccessDataResult<int>(randomNumber);
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

        public IDataResult<User> UserExists(string email)
        {
            var userExists = _userService.GetByEmail(email);
            if (!userExists.Success)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }
            return new SuccessDataResult<User>(userExists.Data);
        }
    }
}
