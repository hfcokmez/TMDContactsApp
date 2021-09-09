using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Contents;
using Core.Utilities.Results;
using TMDContactsApp.DataAccess.Abstract;
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

        public async Task<IResult> AddAsync(User user)
        {
            if (await _unitOfWork.Users.AddAsync(user, "AddUser"))
            {
                return new SuccessResult(Messages.UserAddSuccess);
            }
            return new ErrorResult(Messages.UserAddFail);
        }

        public async Task<IResult> DeleteAsync(int user)
        {
            if (await _unitOfWork.Users.DeleteAsync(user, "DeleteUser"))
            {
                return new SuccessResult(Messages.UserDeleteSuccess);
            }
            return new ErrorResult(Messages.UserDeleteFail);
        }

        public async Task<IResult> DeleteListAsync(List<int> users)
        {
            if (await _unitOfWork.Users.DeleteListAsync(users, "DeleteUser"))
            {
                return new SuccessResult(Messages.UsersDeleteSuccess);
            }
            return new ErrorResult(Messages.UserDeleteFail);
        }

        public async Task<IDataResult<User>> GetByEmailAsync(string email)
        {
            var user = await _unitOfWork.Users.GetAsync(new { @Email = email }, "GetUserByEmail");
            if (user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public async Task<IDataResult<User>> GetByIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetAsync(new { @Id = userId }, "GetUser");
            if (user != null)
            {
                return new SuccessDataResult<User>(user);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public async Task<IDataResult<User>> GetByTelAsync(string tel)
        {
            var user = await _unitOfWork.Users.GetAsync(new { @Tel = tel }, "GetUserByTel");
            if (user != null)
            {
                return new SuccessDataResult<User>(user, Messages.UserTelAlreadyExist);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public async Task<IDataResult<List<User>>> GetListAsync()
        {
            try
            {
                var userList = await _unitOfWork.Users.GetListAsync("GetAllUsers");
                return new SuccessDataResult<List<User>>(userList.ToList());
            }
            catch (ArgumentNullException)
            {
                return new ErrorDataResult<List<User>>(Messages.UserGetFail);
            }
        }

        public async Task<List<OperationClaim>> GetUserOperationClaims(User user)
        {
            return await _unitOfWork.Users.GetClaims(user.Id, "GetClaims");
        }

        public async Task<IResult> UpdateAsync(User user)
        {
            var result = await GetByIdAsync(user.Id);
            if (!result.Success) return result;
            user.PasswordHash = null;
            user.PasswordSalt = null;
            if (await _unitOfWork.Users.UpdateAsync(user, "UpdateUser")) return new SuccessResult(Messages.UserUpdateSuccess);
            return new ErrorResult(Messages.UserUpdateFail);
        }
    }
}
