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
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(User user)
        {
            if (await _unitOfWork.Users.Add(user, "AddUser"))
            {
                return new SuccessResult(Messages.UserAddSuccess);
            }
            return new ErrorResult(Messages.UserAddFail);
        }

        public async Task<IResult> Delete(int user)
        {
            if (await _unitOfWork.Users.Delete(user, "DeleteUser"))
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

        public async Task<IDataResult<User>> GetByEmail(string email)
        {
            var user = await _unitOfWork.Users.Get(new { @Email = email }, "GetUserByEmail");
            if(user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public async Task<IDataResult<User>> GetById(int userId)
        {
            var user = await _unitOfWork.Users.Get(new { @Id = userId }, "GetUser");
            if(user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public async Task<IDataResult<User>> GetByTel(string tel)
        {
            var user = await _unitOfWork.Users.Get(new { @Tel = tel }, "GetUserByTel");
            if (user != null)
            {
                return new SuccessDataResult<User>(user, Messages.UserTelAlreadyExist);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public async Task<IDataResult<List<User>>> GetList()
        {
            try
            {
                var userList = await _unitOfWork.Users.GetList("GetAllUsers");
                return new SuccessDataResult<List<User>>(userList.ToList());
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

        public async Task<IResult> Update(User user)
        {
            var result = await GetById(user.Id);
            if (!result.Success) return result;
            user.PasswordHash = null;
            user.PasswordSalt = null;
            if (await _unitOfWork.Users.Update(user, "UpdateUser")) return new SuccessResult(Messages.UserUpdateSuccess);
            return new ErrorResult(Messages.UserUpdateFail);
        }
    }
}
