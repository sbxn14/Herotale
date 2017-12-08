using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Class : IQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int SpeedBonus { get; set; }
        public int Focus { get; set; }
        public string Query { get; set; }

        public Class()
        {
            Query = "SELECT * FROM dbo.Classes";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            AttackBonus = reader.GetInt32(reader.GetOrdinal("Attack Power Bonus"));
            DefenseBonus = reader.GetInt32(reader.GetOrdinal("Defense Bonus"));
            SpeedBonus = reader.GetInt32(reader.GetOrdinal("Speed Bonus"));
            Focus = reader.GetInt32(reader.GetOrdinal("Focus"));
        }
    }
}