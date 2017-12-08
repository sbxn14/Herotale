using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.MSSQL_Repositories
{
    public class MssqlClassRep : IClassRepository
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

        public List<Class> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Class Get(int id)
        {
            SqlDataReader reader = null;
            Class c = new Class();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Classes WHERE Id=@id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
                    c.Id = id;
                    c.Name = reader.GetString(reader.GetOrdinal("Name"));
                    c.AttackBonus = reader.GetInt32(reader.GetOrdinal("Attack Power Bonus"));
                    c.DefenseBonus = reader.GetInt32(reader.GetOrdinal("Defense Bonus"));
                    c.SpeedBonus = reader.GetInt32(reader.GetOrdinal("Speed Bonus"));

                    conn.Close();
                    conn.Dispose();

                    return c;
                }
            }
            conn.Close();
            conn.Dispose();

            return null;
        }


        public bool Update(Class obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Insert(Class obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Class obj)
        {
            throw new System.NotImplementedException();
        }
    }
}