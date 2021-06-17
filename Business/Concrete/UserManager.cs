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
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAddSuccess);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleteSuccess);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(p => p.Email == email);
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(p => p.Id == userId));
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetList().ToList());
        }

        public List<OperationClaim> GetUserOperationClaims(User user)
        {
            return new List<OperationClaim>(_userDal.GetClaims(user));
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdateSuccess);
        }
    }
}
