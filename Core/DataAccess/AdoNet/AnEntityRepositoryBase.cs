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
        }
        public bool Update(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataset = new DataSet();
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
        }

        public bool Delete(TEntity entity, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataset = new DataSet();
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
        }

        public TEntity Get(int Id, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        }

        public IList<TEntity> GetList(string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

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
        }

        //Done by this line!________________________________________________________________________

        public IList<TEntity> GetList(int Id, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
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
        }

        public IList<TEntity> GetList(int pageNumber, int pageSize, string sProcedure)
        {
            throw new NotImplementedException();
        }

    }
}
