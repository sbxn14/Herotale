using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Item : IQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int SpeedBonus { get; set; }
        public string Query { get; set; }

        public Item()
        {
            Query = "SELECT * FROM dbo.Items";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            AttackBonus = reader.GetInt32(reader.GetOrdinal("Attack Power Bonus"));
            DefenseBonus = reader.GetInt32(reader.GetOrdinal("Defense Bonus"));
            SpeedBonus = reader.GetInt32(reader.GetOrdinal("Speed Bonus"));
        }
    }
}