﻿using System;
using Core.DataAccess.EntityFramework;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using System.Linq;
using Core.Entities.Concrete;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User, TMDContactsAppContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new TMDContactsAppContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}