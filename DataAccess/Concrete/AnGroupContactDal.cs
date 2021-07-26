using Core.DataAccess.AdoNet;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Concrete
{
    public class AnGroupContactDal: AnEntityRepositoryBase<GroupContact>, IGroupContactDal
    {
        public bool Delete(GroupContact groupContact, string sProcedure)
        {
            using (SqlConnection connection = new SqlConnection(AnEntityRepositoryBase<GroupContact>.connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(sProcedure, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@GroupId", SqlDbType.Int);
                        command.Parameters.Add("@ContactId", SqlDbType.Int);
                        command.Parameters["@GroupId"].Value = (groupContact.GroupId);
                        command.Parameters["@ContactId"].Value = (groupContact.ContactId);
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        connection.Close();
                        if (result > 0) return true;
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
    }
}
