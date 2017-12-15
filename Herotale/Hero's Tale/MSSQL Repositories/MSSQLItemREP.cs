using Herotale.IRepositories;
using Herotale.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Herotale.MSSQL_Repositories
{
	public class MssqlItemRep : IItemRepository
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

        public Item Get(int id)
        {
			SqlDataReader reader = null;
			Item r = new Item();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Items WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@id", id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					r.Id = id;
					r.Name = reader.GetString(reader.GetOrdinal("Name"));
					r.AttackBonus = reader.GetInt32(reader.GetOrdinal("Attack Power Bonus"));
					r.DefenseBonus = reader.GetInt32(reader.GetOrdinal("Defense Bonus"));
					r.SpeedBonus = reader.GetInt32(reader.GetOrdinal("Speed Bonus"));

					conn.Close();
					conn.Dispose();

					return r;
				}
			}
			conn.Close();
			conn.Dispose();

			return null;
		}

		public bool Update(Item obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Item obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Item obj)
        {
            throw new System.NotImplementedException();
        }

		public Item GetEmptyItem()
		{
			SqlDataReader reader = null;
			Item i = new Item();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Items WHERE Name=@name", conn);
			cmd.Parameters.AddWithValue("@name", "Empty");
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					i.Id = reader.GetInt32(reader.GetOrdinal("Id"));
					i.Name = reader.GetString(reader.GetOrdinal("Name"));
					i.AttackBonus = reader.GetInt32(reader.GetOrdinal("Attack Power Bonus"));
					i.DefenseBonus = reader.GetInt32(reader.GetOrdinal("Defense Bonus"));
					i.SpeedBonus = reader.GetInt32(reader.GetOrdinal("Speed Bonus"));

					conn.Close();
					conn.Dispose();
					return i;
				}
			}
			conn.Close();
			conn.Dispose();
			return null;
		}
	}

}