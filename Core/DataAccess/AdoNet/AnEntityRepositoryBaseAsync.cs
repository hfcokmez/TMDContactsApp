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
    public class AnEntityRepositoryBaseAsync<TEntity> : IEntityRepositoryAsync<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected readonly static string connectionString = @"Server=TESTWEBDB\TESTWEBDB02;Database=TMDContacts;User Id=db_testadmin;Password=sabahsoft;Trusted_Connection=False;MultipleActiveResultSets=true;";

        public async Task<bool> AddAsync(TEntity entity, string sProcedure)
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
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public async Task<bool> UpdateAsync(TEntity entity, string sProcedure)
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
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public async Task<IList<TEntity>> GetListAsync(string sProcedure)
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
                        return entityList;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public async Task<bool> DeleteAsync(dynamic dto, string sProcedure)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction tran = connection.BeginTransaction())
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
        public async Task<bool> DeleteListAsync(IList<int> entityList, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand(sProcedure, connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Id", SqlDbType.Int);
                        foreach (int entity in entityList)
                        {
                            command.Parameters["@Id"].Value = (entity);
                            int result = await command.ExecuteNonQueryAsync();
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
        public async Task<IList<TEntity>> GetListAsync(dynamic dto, string sProcedure)
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
        public async Task<TEntity> GetAsync(dynamic dto, string sProcedure)
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
                        return Entity;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public async Task<int> GetCountAsync(dynamic dto, string propertyName, string sProcedure)
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
                        if (dataReader.HasRows) await dataReader.ReadAsync();
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
