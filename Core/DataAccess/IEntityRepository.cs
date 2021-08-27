using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> Get(dynamic dto, string sProcedure);
        int GetCount(dynamic dto, string propertyName, string sProcedure);
        Task<IList<T>> GetList(string sProcedure);
        Task<IList<T>> GetList(dynamic dto, string sProcedure);
        Task<bool> Add(T entity, string sProcedure);
        Task<bool> Delete(int entity, string sProcedure);
        bool Delete(IList<int> entityList, string sProcedure);
        Task<bool> Update(T entity, string sProcedure);
    }
}
