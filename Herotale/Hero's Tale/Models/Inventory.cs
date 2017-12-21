using System.Data.SqlClient;
using System.Linq;
using Herotale.Database;
using System.Collections.Generic;
using System;

namespace Herotale.Models
{

	public class Inventory
	{
		public int Id { get; set; }
		public List<Item> Items { get; set; }
	}
}