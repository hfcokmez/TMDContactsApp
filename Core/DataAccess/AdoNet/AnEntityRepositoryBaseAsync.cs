using Core.Entities.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMDContactsApp.Core.Entities.Concrete;

namespace Core.DataAccess.AdoNet
{
    public class AnEntityRepositoryBaseAsync<TEntity> : IEntityRepositoryAsync<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly ConnectionSettings _connectionSettings;
        public AnEntityRepositoryBaseAsync(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }
        public async Task<bool> AddAsync(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
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
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
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
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
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
        public async Task<bool> DeleteAsync(dynamic param, string sProcedure)
        {

            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction tran = connection.BeginTransaction())
                using (var command = new SqlCommand(sProcedure, connection, tran))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        foreach (var property in param.GetType().GetProperties())
                    {
                        if (property.GetValue(param, null) != null)
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(param, null));
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
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
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
        public async Task<IList<TEntity>> GetListAsync(dynamic param, string sProcedure)
        {
            var entityList = new List<TEntity>();
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in param.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(param, null));
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
        public async Task<TEntity> GetAsync(dynamic param, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in param.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(param, null));
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
        public async Task<int> GetCountAsync(dynamic param, string propertyName, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var property in param.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(param, null));
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
