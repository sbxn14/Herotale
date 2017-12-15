using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Class 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int SpeedBonus { get; set; }
        public int Focus { get; set; }
        public string Query { get; set; }
    }
}