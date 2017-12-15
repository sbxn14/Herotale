using System;
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
