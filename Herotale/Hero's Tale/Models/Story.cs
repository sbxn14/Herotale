using Herotale.Database;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Herotale.Models
{
	public class Story
	{
		public Segment Sgt = new Segment();
		public Character Char = new Character();   //has checkpoint
	}

	public class Segment : IQuery
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string Query { get; set; }

		public Segment()
		{
			Query = "SELECT * FROM dbo.Stories";
		}

		public void Parse(SqlDataReader reader)
		{
			Title = reader.GetString(reader.GetOrdinal("Title"));
			Text = reader.GetString(reader.GetOrdinal("Text"));
			Id = reader.GetInt32(reader.GetOrdinal("Id"));
		}
	}
}
