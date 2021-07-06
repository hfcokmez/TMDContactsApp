using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Core.DataAccess.AdoNet
{
    public class AnEntityRepositoryBase<TEntity> : EEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly static string connectionString = @"Server=TESTWEBDB\TESTWEBDB02;Database=TMDContacts;User Id=db_testadmin;Password=sabahsoft;Trusted_Connection=False;MultipleActiveResultSets=true;";

        public bool Add(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataset = new DataSet();
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var property in entity.GetType().GetProperties())
                        {
                            if (String.Equals(property.Name, "Id")) continue;
                            if (property.GetValue(entity, null) != null)
                            {
                                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity, null));
                                continue;
                            }
                            else command.Parameters.AddWithValue($"@{property.Name}", DBNull.Value);
                        }
                        new SqlDataAdapter(command).Fill(dataset);
                    }
                    connection.Close();
                    return true;
                }
                catch (Exception)
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public bool Update(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataset = new DataSet();
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var property in entity.GetType().GetProperties())
                        {
                            if (property.GetValue(entity, null) != null)
                            {
                                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity, null));
                                continue;
                            }
                        }
                        new SqlDataAdapter(command).Fill(dataset);
                    }
                    connection.Close();
                    return true;
                }
                catch (Exception)
                {
                    connection.Close();
                    return false;
                }
            }
        }



        public TEntity Get(int Id, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", Id);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                        }
                        var Entity = new TEntity();
                        object boxedObject = RuntimeHelpers.GetObjectValue(Entity);

                        foreach (var property in Entity.GetType().GetProperties())
                        {
                            if (dataReader[property.Name] != DBNull.Value)
                            {
                                property.SetValue(boxedObject, dataReader[property.Name]);
                                continue;
                            }
                            property.SetValue(boxedObject, null);
                        }
                        Entity = (TEntity)boxedObject;
                        connection.Close();
                        return Entity;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public TEntity Get(string email, string sProcedure)
        {
            //var get = new TEntity();
            //var p = new DynamicParameters();
            //p.Add("@Email", email);
            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    get = db.Query<TEntity>(sProcedure, p, commandType: CommandType.StoredProcedure);
            //}
            //return get;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                        }
                        else
                        {
                            return null;
                        }
                        var Entity = new TEntity();
                        object boxedObject = RuntimeHelpers.GetObjectValue(Entity);

                        foreach (var property in Entity.GetType().GetProperties())
                        {
                            if (dataReader[property.Name] != DBNull.Value)
                            {
                                property.SetValue(boxedObject, dataReader[property.Name]);
                                continue;
                            }
                            property.SetValue(boxedObject, null);
                        }
                        Entity = (TEntity)boxedObject;
                        connection.Close();
                        return Entity;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return null;
                }
            }
        }

        public IList<TEntity> GetList(string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dataReader = command.ExecuteReader();
                        var entityList = new List<TEntity>();
                        if (!dataReader.HasRows)
                        {
                            connection.Close();
                            return null;
                        }
                        while (dataReader.Read())
                        {
                            var entity = new TEntity();
                            object boxedObject = RuntimeHelpers.GetObjectValue(entity);
                            foreach (var property in entity.GetType().GetProperties())
                            {
                                if (dataReader[property.Name] != DBNull.Value)
                                {
                                    property.SetValue(boxedObject, dataReader[property.Name]);
                                    continue;
                                }
                                property.SetValue(boxedObject, null);
                            }
                            entity = (TEntity)boxedObject;
                            entityList.Add(entity);
                        }
                        connection.Close();
                        return entityList;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return null;
                }
            }
        }
        //public IList<TEntity> GetList(string sProcedure)
        //{
        //    List<TEntity> get = new List<TEntity>();
        //    using (IDbConnection db = new SqlConnection(connectionString))
        //    {
        //        get = db.Query<TEntity>(sProcedure, commandType: CommandType.StoredProcedure).ToList();
        //    }
        //    return get;
        //}

        public IList<TEntity> GetList(int id, string field, string sProcedure)
        {
            //var p = new DynamicParameters();
            //p.Add($"@{field}", id);
            //List<TEntity> getBy = new List<TEntity>();
            //using (IDbConnection db = new SqlConnection(connectionString))
            //{
            //    getBy = db.Query<TEntity>(sProcedure, p, commandType: CommandType.StoredProcedure).ToList();
            //}
            //return getBy;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"@{field}", id);
                        SqlDataReader dataReader = command.ExecuteReader();
                        var entityList = new List<TEntity>();
                        while (dataReader.Read())
                        {
                            var entity = new TEntity();
                            object boxedObject = RuntimeHelpers.GetObjectValue(entity);
                            foreach (var property in entity.GetType().GetProperties())
                            {
                                if (dataReader[property.Name] != DBNull.Value)
                                {
                                    property.SetValue(boxedObject, dataReader[property.Name]);
                                    continue;
                                }
                                property.SetValue(boxedObject, null);
                            }
                            entity = (TEntity)boxedObject;
                            entityList.Add(entity);
                        }
                        connection.Close();
                        return entityList;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return null;
                }

            }
        }
        public bool Delete(int entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int);
                        command.Parameters["@Id"].Value = (entity);
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                        if (result > 0)return true;
                        else return false;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return false;
                }
            }
        }
        public bool Delete(IList<int> entityList, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(sProcedure, connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int);
                        foreach (int entity in entityList)
                        {
                            command.Parameters["@Id"].Value = (entity);
                            int result = command.ExecuteNonQuery();
                            if (result <= 0)
                            {
                                transaction.Rollback();
                                connection.Close();
                                return false;
                            }
                        }
                        transaction.Commit();
                        connection.Close();
                        return true;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    connection.Close();
                    return false;
                }
            }
        }
    }
}
