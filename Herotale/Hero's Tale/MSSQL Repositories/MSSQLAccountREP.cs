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
            return DB.RunQuery(new Account());
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

            if (CheckAdmin(acc))
            {
                //log in as admin
            }

            if (reader != null && reader.HasRows)
            {
                return true;
            }
            return false;
        }

        public string IdGetter(Account acc)
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
                while (reader.Read())
                {
                    Account a = new Account();
                    a.Parse(reader);
                    Pk = a.Id;
                }
                return Pk.ToString();
            }
            return null;
        }

        public bool CheckAdmin(Account acc)
        {
            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Accounts WHERE emailaddress=@email AND password=@password AND rights=1", conn);
            cmd.Parameters.AddWithValue("@email", acc.Email);
            cmd.Parameters.AddWithValue("@password", acc.Password);
            reader = cmd.ExecuteReader();

            if (reader != null && reader.HasRows)
            {
                return true;
            }
            return false;
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
            throw new NotImplementedException();
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