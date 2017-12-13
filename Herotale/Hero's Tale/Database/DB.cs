﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Herotale.Database
{
    public static class DB
    {
        public static string ConnectionString { get; set; }
        
        static DB()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }

        public static List<T> RunQuery<T>(T value) where T : IQuery, new()
        {
            SqlDataReader reader = null;
            List<T> result = new List<T>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = value.Query;

                    try
                    {
                        cmd.Connection.Open();
                        cmd.Prepare();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            T obj = new T();
                            obj.Parse(reader);
                            result.Add(obj);
                        }
                    }
                    catch (SqlException e)
                    {
                        Console.Write(e);
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        reader?.Close();
                    }
                    return result;
                }
            }
        }
        
        public static bool RunNonQuery(SqlCommand com)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = com)
                {
                    cmd.Connection = con;
                    try
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }
        }
    }
}
