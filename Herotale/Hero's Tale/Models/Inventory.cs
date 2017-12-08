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
        public List<Int32> Slots { get; set; }
        public List<Item> Items { get; set; }
        public string Query { get; set; }

        public Inventory()
        {
            Query = "SELECT * FROM dbo.Inventory";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Slots[0] = reader.GetInt32(reader.GetOrdinal("item1id"));
            Slots[1] = reader.GetInt32(reader.GetOrdinal("item2id"));
            Slots[2] = reader.GetInt32(reader.GetOrdinal("item3id"));
            Slots[3] = reader.GetInt32(reader.GetOrdinal("item4id"));
            Slots[4] = reader.GetInt32(reader.GetOrdinal("item5id"));
            Slots[5] = reader.GetInt32(reader.GetOrdinal("item6id"));
            Slots[6] = reader.GetInt32(reader.GetOrdinal("item7id"));
            Slots[7] = reader.GetInt32(reader.GetOrdinal("item8id"));
            Slots[8] = reader.GetInt32(reader.GetOrdinal("item9id"));
            Slots[9] = reader.GetInt32(reader.GetOrdinal("item10id"));

            Items[0] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[0]);
            Items[1] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[1]);
            Items[2] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[2]);
            Items[3] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[3]);
            Items[4] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[4]);
            Items[5] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[5]);
            Items[6] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[6]);
            Items[7] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[7]);
            Items[8] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[8]);
            Items[9] = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slots[9]);
        }
    }
}