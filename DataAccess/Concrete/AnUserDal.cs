using Core.DataAccess.AdoNet;
using Core.Entities.Concrete;
using TMDContactsApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TMDContactsApp.Core.Entities.Concrete;

namespace DataAccess.Concrete
{
    public class AnUserDal : AnEntityRepositoryBaseAsync<User>, IUserDal
    {
        private readonly ConnectionSettings _connectionSettings;
        public AnUserDal(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }
        public async Task<List<OperationClaim>> GetClaims(int userId, string sProcedure)
        {
            var entityList = new List<OperationClaim>();
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                            command.Parameters.AddWithValue("@UserId", userId);

                        SqlDataReader dataReader = await command.ExecuteReaderAsync();
                        while (await dataReader.ReadAsync())
                        {
                            var entity = new OperationClaim();
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
                            entity = (OperationClaim)boxedObject;
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
    }
}
