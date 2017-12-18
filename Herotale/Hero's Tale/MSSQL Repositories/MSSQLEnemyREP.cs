using Herotale.IRepositories;
using Herotale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Herotale.MSSQL_Repositories
{
    public class MssqlEnemyRep : IEnemyRepository
    {
        public bool Update()
        {
            throw new System.NotImplementedException();
        }

        public bool Insert()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove()
        {
            throw new System.NotImplementedException();
        }

        public Enemy Get(int id)
		{
			SqlDataReader reader = null;
			Enemy r = new Enemy();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Enemies WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					r.Id = id;
					r.Name = reader.GetString(reader.GetOrdinal("Name"));
					r.AttackPower = reader.GetInt32(reader.GetOrdinal("Attack Power"));
					r.Health = reader.GetInt32(reader.GetOrdinal("Health"));

					conn.Close();
					conn.Dispose();

					return r;
				}
			}
			conn.Close();
			conn.Dispose();

			return null;
		}

		public bool Update(Enemy obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Enemy obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Enemy obj)
        {
            throw new System.NotImplementedException();
        }

		public int Randomizer()
		{
			Random r = new Random();
			return r.Next(0, (GetAll().Count - 1));
		}
		public List<Enemy> GetAll()
		{
			SqlDataReader reader = null;
			List<Enemy> result = new List<Enemy>();
			string query = "Select * from dbo.Enemies";

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
							Enemy r = new Enemy();
							r.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							r.Name = reader.GetString(reader.GetOrdinal("Name"));
							r.AttackPower = reader.GetInt32(reader.GetOrdinal("Attack Power"));
							r.Health = reader.GetInt32(reader.GetOrdinal("Health"));
							result.Add(r);
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

		public Enemy RandomMob(int randomnumber, List<Enemy> Enemies)
		{
			return Enemies[randomnumber];
		}
	}
}