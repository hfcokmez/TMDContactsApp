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
                            if (String.Equals(property.Name, "Id"))
                            { continue; }
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity, null));
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
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity, null));
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

        public bool Delete(TEntity entity, string sProcedure)
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
                            if (String.Equals(property.Name, "Id"))
                            {
                                command.Parameters.AddWithValue("@Id", property.GetValue(entity, null));
                                break;
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
                            property.SetValue(boxedObject, dataReader[property.Name]);
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

        public TEntity Get(string Email, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", Email);
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
                            property.SetValue(boxedObject, dataReader[property.Name]);
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
                                property.SetValue(boxedObject, dataReader[property.Name]);
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

        public IList<TEntity> GetList(int userId, string field, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(String.Format("@{0}", field), userId);
                        SqlDataReader dataReader = command.ExecuteReader();
                        var entityList = new List<TEntity>();
                        while (dataReader.Read())
                        {
                            var entity = new TEntity();
                            object boxedObject = RuntimeHelpers.GetObjectValue(entity);
                            foreach (var property in entity.GetType().GetProperties())
                            {
                                property.SetValue(boxedObject, dataReader[property.Name]);
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

        public bool DeleteList(IList<TEntity> entityList, string sProcedure)
        {
            throw new NotImplementedException();
        }
    }
}
