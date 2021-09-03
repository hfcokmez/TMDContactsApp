using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Core.DataAccess.AdoNet
{
    public class AnEntityRepositoryBaseSync<TEntity> : IEntityRepositorySync<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly static string connectionString = @"Server=TESTWEBDB\TESTWEBDB02;Database=TMDContacts;User Id=db_testadmin;Password=sabahsoft;Trusted_Connection=False;MultipleActiveResultSets=true;";

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
                    return true;
                }
                catch (Exception)
                {
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
                            }
                            else if (property.PropertyType == typeof(System.Byte[]))
                            {
                                command.Parameters.Add($"@{property.Name}", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                            }
                            else command.Parameters.AddWithValue($"@{property.Name}", DBNull.Value);
                        }
                        var a = new SqlDataAdapter(command).Fill(dataset);
                        return true;
                    }
                }
                catch (Exception )
                {
                    return false;
                }
            }
        }
        public TEntity Get(int id, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);
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
                        return Entity;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TEntity Get(string parameter, string field, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"@{field}", parameter);
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows)   dataReader.Read();
                        else   return null;
                       
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
                        return Entity;
                    }
                }
                catch (Exception)
                {
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
                        return entityList;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public IList<TEntity> GetList(int id, string field, string sProcedure)
        {
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
                        if (entityList.Any()) return entityList;
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public bool Delete(dynamic dto, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var tran = connection.BeginTransaction())
                using (var command = new SqlCommand(sProcedure, connection, tran))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        foreach (var property in dto.GetType().GetProperties())
                        {
                            if (property.GetValue(dto, null) != null)
                            {
                                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(dto, null));
                            }
                        }
                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        tran.Rollback();
                        return false;
                    }
                    tran.Commit();
                    return true;
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
                                return false;
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
        public IList<TEntity> GetList(dynamic dto, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in dto.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(dto, null));
                        }
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
                        if (entityList.Any()) return entityList;
                        return null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public TEntity Get(dynamic dto, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in dto.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(dto, null));
                        }
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
                        return Entity;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public int GetCount(dynamic dto, string propertyName, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in dto.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(dto, null));
                        }
                        SqlDataReader dataReader = command.ExecuteReader();
                        if (dataReader.HasRows) dataReader.Read();
                        var result = dataReader.GetInt32(0);
                        return result;
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}