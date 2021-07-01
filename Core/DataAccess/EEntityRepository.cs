using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Core
{
    public interface EEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(int id, string sProcedure);
        T Get(string email, string sProcedure);
        IList<T> GetList(string sProcedure);
        IList<T> GetList(int id, string parameter, string sProcedure);
        bool Add(T entity, string sProcedure);
        bool Delete(T entity, string sProcedure);
        bool DeleteByGroup(IList<T> entityList, string sProcedure);
        bool Update(T entity, string sProcedure);
    }
}
