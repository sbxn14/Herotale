using System.Data.SqlClient;
using System.Linq;
using Herotale.Database;
using System.Collections.Generic;
using System;

namespace Herotale.Models
{
	public class Inventory : IQuery
	{
		public int Id { get; set; }
		public List<int> Slots { get; set; }
		public List<Item> Items { get; set; }
		public string Query { get; set; }

		public Inventory()
		{
			Query = "SELECT * FROM dbo.Inventory";
		}

		public void Parse(SqlDataReader reader)
		{
			Slots = new List<int>();
			Items = new List<Item>();
			Id = reader.GetInt32(reader.GetOrdinal("Id"));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item1id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item2id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item3id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item4id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item5id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item6id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item7id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item8id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item9id")));
			Slots.Add(reader.GetInt32(reader.GetOrdinal("item10id")));

			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[0]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[1]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[2]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[3]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[4]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[5]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[6]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[7]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[8]));
			Items.Add(Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[9]));
		}
	}
}