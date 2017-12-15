using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Enemy 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AttackPower { get; set; }
        public int Health { get; set; }
    }
}