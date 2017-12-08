using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Enemy : IQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttackPower { get; set; }
        public int Health { get; set; }
        public string Query { get; set; }

        public Enemy()
        {
            Query = "SELECT * FROM dbo.Enemies";
        }

        public Enemy(string name, int attackpower, int health)
        {
            Name = name;
            AttackPower = attackpower;
            Health = health;
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            AttackPower = reader.GetInt32(reader.GetOrdinal("AttackPower"));
            Health = reader.GetInt32(reader.GetOrdinal("Health"));
        }
    }
}