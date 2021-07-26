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
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            if (_userDal.Add(user, "AddUser"))
            {
                return new SuccessResult(Messages.UserAddSuccess);
            }
            return new ErrorResult(Messages.UserAddFail);
        }

        public IResult Delete(int user)
        {
            if (_userDal.Delete(user, "DeleteUser"))
            {
                return new SuccessResult(Messages.UserDeleteSuccess);
            }
            return new ErrorResult(Messages.UserDeleteFail);
        }

        public IResult Delete(List<int> users)
        {
            if (_userDal.Delete(users, "DeleteUser"))
            {
                return new SuccessResult(Messages.UsersDeleteSuccess);
            }
            return new ErrorResult(Messages.UserDeleteFail);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = _userDal.Get(email, "Email", "GetUserByEmail");
            if(user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<User> GetById(int userId)
        {
            var user = _userDal.Get(userId, "GetUser");
            if(user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<User> GetByTel(string tel)
        {
            var user = _userDal.Get(tel, "Tel", "GetUserByTel");
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
                var userList = _userDal.GetList("GetAllUsers").ToList();
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
            if(_userDal.Update(user, "UpdateUser")) return new SuccessResult(Messages.UserUpdateSuccess);
            return new ErrorResult(Messages.UserUpdateFail);
        }
    }
}
