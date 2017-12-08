using System.Data.SqlClient;

namespace Herotale.Database
{
    public interface IQuery
    {
        string Query { get; set; }
        void Parse(SqlDataReader reader);
    }
}