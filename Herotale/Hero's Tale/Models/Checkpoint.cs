using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }
        public int Event { get; set; }
    }
}