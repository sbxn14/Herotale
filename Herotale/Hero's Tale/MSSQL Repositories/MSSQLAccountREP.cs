using Herotale.Database;
using Herotale.IRepositories;
using Herotale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Herotale.MSSQL_Repositories
{
    public class MssqlAccountRep : IAccountRepository
    {
        public int Pk { get; set; }

		public List<Account> GetAll()
		{
			SqlDataReader reader = null;
			List<Account> result = new List<Account>();
			string query = "Select * from dbo.Accounts";

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
							Account obj = new Account();
							obj.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							obj.Email = reader.GetString(reader.GetOrdinal("Emailaddress"));
							obj.Password = reader.GetString(reader.GetOrdinal("Password"));
							obj.Rights = reader.GetBoolean(reader.GetOrdinal("Rights"));
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

		public bool RegisterChecker(string mail)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from dbo.Accounts where emailaddress=@email", conn);
            cmd.Parameters.AddWithValue("@email", mail);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                return true;
            }
                return false;
        }

        public bool LoginChecker(Account acc)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Accounts WHERE emailaddress=@email AND password=@password", conn);
            cmd.Parameters.AddWithValue("@email", acc.Email);
            cmd.Parameters.AddWithValue("@password", acc.Password);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                return true;
            }
            return false;
        }

        public string IdGetter(Account acc)
        {
            SqlDataReader reader = null;
			Account a = new Account();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Accounts WHERE emailaddress=@email AND password=@password", conn);
            cmd.Parameters.AddWithValue("@email", acc.Email);
            cmd.Parameters.AddWithValue("@password", acc.Password);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                while (reader.Read())
                {
					a.Id = reader.GetInt32(reader.GetOrdinal("Id"));
					a.Email = reader.GetString(reader.GetOrdinal("Emailaddress"));
					a.Password = reader.GetString(reader.GetOrdinal("Password"));
					a.Rights = reader.GetBoolean(reader.GetOrdinal("Rights"));
					
                }
                return a.Id.ToString();
            }
            return null;
        }
		
		public Account Get(int id)
		{
			SqlDataReader reader = null;
			Account r = new Account();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();

			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Accounts WHERE Id=@id", conn);

			cmd.Parameters.AddWithValue("@id", id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					r.Id = id;
					r.Email = reader.GetString(reader.GetOrdinal("Emailaddress"));
					r.Password = reader.GetString(reader.GetOrdinal("Password"));
					r.Rights = reader.GetBoolean(reader.GetOrdinal("Rights"));

					conn.Close();
					conn.Dispose();

					return r;
				}
			}
			conn.Close();
			conn.Dispose();

			return null;
		}
		public bool Insert(Account obj)
        {
            string mail = obj.Email;
            string pass = obj.Password;
            bool right = obj.Rights;

            bool check = RegisterChecker(mail);

            SqlCommand com = new SqlCommand();

            string query = "INSERT INTO dbo.Accounts (Emailaddress, Password, Rights) VALUES (@email, @password, @rights)";

            com.CommandText = query;

            com.Parameters.AddWithValue("@email", mail);
            com.Parameters.AddWithValue("@password", pass);
            com.Parameters.AddWithValue("@rights", right);


            if (check == false)
            {
                return DB.RunNonQuery(com);
            }
            
            return false;
        }

        public bool Remove(Account obj)
        {
			string query = "DELETE FROM dbo.Accounts WHERE id=@id";
			SqlCommand cmd = new SqlCommand(query);

			cmd.Parameters.AddWithValue("@id", obj.Id);

			return DB.RunNonQuery(cmd);
		}

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Update(Account obj)
        {
            throw new NotImplementedException();
        }
    }
}