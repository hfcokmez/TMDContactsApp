using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Core.DataAccess.AdoNet
{
    public class AnEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly static string connectionString = @"Server=TESTWEBDB\TESTWEBDB02;Database=TMDContacts;User Id=db_testadmin;Password=sabahsoft;Trusted_Connection=False;MultipleActiveResultSets=true;";

        public async Task<bool> Add(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataset = new DataSet();
                try
                {
                    await connection.OpenAsync();
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
                        var dAdapter = new SqlDataAdapter(command);
                        await Task.Run(() => dAdapter.Fill(dataset));
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

        public async Task<bool> Update(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataset = new DataSet();
                try
                {
                    await connection.OpenAsync();
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
                        var dAdapter = new SqlDataAdapter(command);
                        await Task.Run(() => dAdapter.Fill(dataset));
                        connection.Close();
                        return true;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public async Task<IList<TEntity>> GetList(string sProcedure)
        {
            var entityList = new List<TEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (!reader.HasRows)
                                connection.Close();
                            while (await reader.ReadAsync())
                            {
                                var entity = new TEntity();
                                object boxedObject = RuntimeHelpers.GetObjectValue(entity);
                                foreach (var property in entity.GetType().GetProperties())
                                {
                                    if (reader[property.Name] != DBNull.Value)
                                    {
                                        property.SetValue(boxedObject, reader[property.Name]);
                                        continue;
                                    }
                                    property.SetValue(boxedObject, null);
                                }
                                entity = (TEntity)boxedObject;
                                entityList.Add(entity);
                            }
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


        public async Task<bool> Delete(int entity, string sProcedure)
        {
           
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var tran = connection.BeginTransaction())
                using (var command = new SqlCommand(sProcedure, connection, tran))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Id", SqlDbType.Int);
                    command.Parameters["@Id"].Value = (entity);
                    try
                    {
                        await command.ExecuteNonQueryAsync();
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

        public async Task<IList<TEntity>> GetList(dynamic dto, string sProcedure)
        {
            var entityList = new List<TEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in dto.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(dto, null));
                        }
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (await dataReader.ReadAsync())
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
                        if (entityList.Any()) return entityList;
                        return null;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return null;
                }
            }
        }
        public async Task<TEntity> Get(dynamic dto, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in dto.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(dto, null));
                        }
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        if (dataReader.HasRows)
                        {
                            await dataReader.ReadAsync();
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
                        connection.Close();
                        return result;
                    }
                }
                catch (Exception)
                {
                    connection.Close();
                    return 0;
                }
            }
        }
    }
}
