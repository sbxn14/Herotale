using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Herotale.Database;

namespace Herotale.Models
{
    public class Account 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Rights { get; set; }
    }
}
