using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Account : IQuery
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Rights { get; set; }
        public string Query { get; set; }

        public Account()
        {
            Query = "Select * From dbo.Accounts";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Email = reader.GetString(reader.GetOrdinal("Emailaddress"));
            Password = reader.GetString(reader.GetOrdinal("Password"));
            Rights = reader.GetBoolean(reader.GetOrdinal("Rights"));
        }
    }
}
