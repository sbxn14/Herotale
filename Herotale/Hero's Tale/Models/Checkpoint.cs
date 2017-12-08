using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Checkpoint : IQuery
    {
        public int Id { get; set; }
        public int Event { get; set; }
        public string Query { get; set; }

        public Checkpoint()
        {
            Query = "SELECT * FROM dbo.Checkpoints";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Event = reader.GetInt32(reader.GetOrdinal("Event"));
        }
    }
}