using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEntityRepositoryAsync<T> where T : class, IEntity, new()
    {
        Task<T> GetAsync(dynamic dto, string sProcedure);
        Task<int> GetCountAsync(dynamic dto, string propertyName, string sProcedure);
        Task<IList<T>> GetListAsync(string sProcedure);
        Task<IList<T>> GetListAsync(dynamic dto, string sProcedure);
        Task<bool> AddAsync(T entity, string sProcedure);
        Task<bool> DeleteAsync(dynamic dto, string sProcedure);
        Task<bool> DeleteListAsync(IList<int> entityList, string sProcedure);
        Task<bool> UpdateAsync(T entity, string sProcedure);
    }
}
