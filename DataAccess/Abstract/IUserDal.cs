using Core;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TMDContactsApp.DataAccess.Abstract
{
    public interface IUserDal: IEntityRepositoryAsync<User>
    {
        Task<List<OperationClaim>> GetClaims(int userId, string sProcedure);
    }
}
