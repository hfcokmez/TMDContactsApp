using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IResult Add(User user)
        {
            if (_unitOfWork.Users.Add(user, "AddUser"))
            {
                return new SuccessResult(Messages.UserAddSuccess);
            }
            return new ErrorResult(Messages.UserAddFail);
        }

        public IResult Delete(int user)
        {
            if (_unitOfWork.Users.Delete(user, "DeleteUser"))
            {
                return new SuccessResult(Messages.UserDeleteSuccess);
            }
            return new ErrorResult(Messages.UserDeleteFail);
        }

        public IResult Delete(List<int> users)
        {
            if (_unitOfWork.Users.Delete(users, "DeleteUser"))
            {
                return new SuccessResult(Messages.UsersDeleteSuccess);
            }
            return new ErrorResult(Messages.UserDeleteFail);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = _unitOfWork.Users.Get(email, "Email", "GetUserByEmail");
            if(user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<User> GetById(int userId)
        {
            var user = _unitOfWork.Users.Get(userId, "GetUser");
            if(user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<User> GetByTel(string tel)
        {
            var user = _unitOfWork.Users.Get(tel, "Tel", "GetUserByTel");
            if (user != null)
            {
                return new SuccessDataResult<User>(user, Messages.UserTelAlreadyExist);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<List<User>> GetList()
        {
            try
            {
                var userList = _unitOfWork.Users.GetList("GetAllUsers").ToList();
                return new SuccessDataResult<List<User>>(userList);
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<User>>(Messages.UserGetFail);
            }
        }

        public List<OperationClaim> GetUserOperationClaims(User user)
        {
            return null;
        }

        public IResult Update(User user)
        {
            var result = GetById(user.Id);
            if (!result.Success) return result;
            user.PasswordHash = null;
            user.PasswordSalt = null;
            if (_unitOfWork.Users.Update(user, "UpdateUser")) return new SuccessResult(Messages.UserUpdateSuccess);
            return new ErrorResult(Messages.UserUpdateFail);
        }
    }
}
