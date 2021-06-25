using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public interface EEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(int Id, string sProcedure);
        IList<T> GetList(string sProcedure);
        IList<T> GetList(int Id, string sProcedure);
        IList<T> GetList(int pageNumber, int pageSize, string sProcedure);
        bool Add(T entity, string sProcedure);
        bool Delete(T entity, string sProcedure);
        bool Update(T entity, string sProcedure);
    }
}
