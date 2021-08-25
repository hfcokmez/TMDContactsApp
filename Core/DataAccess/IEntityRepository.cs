using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Core
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(int id, string sProcedure);
        T Get(string parameter, string field, string sProcedure);
        T Get(dynamic dto, string sProcedure);
        int GetCount(dynamic dto, string propertyName, string sProcedure);
        IList<T> GetList(string sProcedure);
        IList<T> GetList(int id, string field, string sProcedure);
        IList<T> GetList(dynamic dto, string sProcedure);
        bool Add(T entity, string sProcedure);
        bool Delete(int entity, string sProcedure);
        bool Delete(IList<int> entityList, string sProcedure);
        bool Update(T entity, string sProcedure);
    }
}
