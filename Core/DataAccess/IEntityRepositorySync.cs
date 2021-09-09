using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Core
{
    public interface IEntityRepositorySync<T> where T : class, IEntity, new()
    {
        T Get(int id, string sProcedure);
        T Get(string param, string field, string sProcedure);
        T Get(dynamic param, string sProcedure);
        int GetCount(dynamic param, string propertyName, string sProcedure);
        IList<T> GetList(string sProcedure);
        IList<T> GetList(int id, string field, string sProcedure);
        IList<T> GetList(dynamic param, string sProcedure);
        bool Add(T entity, string sProcedure);
        bool Delete(dynamic param, string sProcedure);
        bool Delete(IList<int> entityList, string sProcedure);
        bool Update(T entity, string sProcedure);
    }
}
