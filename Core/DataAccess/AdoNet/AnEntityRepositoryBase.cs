using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.AdoNet
{
    public class AnEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
               
                string connectionString = context.Database.GetDbConnection().ConnectionString;
                string cmdText = "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        if (sqlCommand.ExecuteNonQuery() > 0)
                        {

                        }
                        connection.Close();
                    }
                }
            }
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            //            using (var context = new TContext())
            //{
            //    var connectionString = context.Database.GetDbConnection().ConnectionString;
            //    SqlConnection sqlConnection = new SqlConnection(connectionString);
            //    string command = "";

            //}
            throw new NotImplementedException();
        }

        public IList<TEntity> GetList(int pageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
