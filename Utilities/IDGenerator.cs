using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace IT508_Project.Utilities
{
    public class IDGenerator
    {
        private string _connectionString;

        public IDGenerator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int GetNextID(string query, int startID)
        {
            int nextID = startID;

            using (SqlConnection sqlcon = new SqlConnection(_connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        nextID = startID;
                    }
                    else
                    {
                        nextID = Convert.ToInt32(reader[0]);
                    }
                    nextID++;
                }
            }

            return nextID;
        }
    }
}