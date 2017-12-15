using Herotale.Contexts;
using Herotale.IRepositories;
using Herotale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Herotale.MSSQL_Repositories
{
	public class MSSQLCheckpointREP : ICheckpointRepository
	{
		public Checkpoint GetById(int id)
		{
			SqlDataReader reader = null;
			Checkpoint r = new Checkpoint();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
<<<<<<< HEAD
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Checkpoints WHERE Id=@id", conn);
=======
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Checkpoint WHERE Id=@id", conn);
>>>>>>> master
			cmd.Parameters.AddWithValue("@id", id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					r.Id = id;
					r.Event = reader.GetInt32(reader.GetOrdinal("Event"));

					conn.Close();
					conn.Dispose();
					return r;
				}
			}
			conn.Close();
			conn.Dispose();
			return null;
		}
		public List<Checkpoint> GetAll()
		{
			SqlDataReader reader = null;
			List<Checkpoint> result = new List<Checkpoint>();
			string query = "Select * from dbo.Checkpoints";

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
			{
				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					try
					{
						cmd.Connection.Open();
						cmd.Prepare();
						reader = cmd.ExecuteReader();

						while (reader.Read())
						{
							Checkpoint obj = new Checkpoint();
							obj.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							obj.Event = reader.GetInt32(reader.GetOrdinal("Event"));
							result.Add(obj);
						}
					}
					catch (SqlException e)
					{
						throw e;
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
	}
}