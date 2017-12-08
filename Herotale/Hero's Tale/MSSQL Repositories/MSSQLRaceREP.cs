using System.Configuration;
using System.Data.SqlClient;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.MSSQL_Repositories
{
    public class MssqlRaceRep : IRaceRepository
    {
        public bool Update()
        {
            throw new System.NotImplementedException();
        }

        public Race Insert()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove()
        {
            throw new System.NotImplementedException();
        }

        public Race Get(int id)
        {
            SqlDataReader reader = null;
            Race r = new Race();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Races WHERE Id=@id", conn);
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

        public bool Update(Race obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Race obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Race obj)
        {
            throw new System.NotImplementedException();
        }
    }
}