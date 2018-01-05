using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Herotale.Contexts;
using Herotale.Database;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.MSSQL_Repositories
{
	public class MssqlInventoryRep : IInventoryRepository
	{
		ItemContext ItemCon = new ItemContext(new MssqlItemRep());

		public bool Update()
		{
			throw new System.NotImplementedException();
		}

		public bool Insert()
		{
			throw new System.NotImplementedException();
		}

		public bool Remove()
		{
			throw new System.NotImplementedException();
		}

		public Inventory Get(int id)
		{
			SqlDataReader reader = null;
			Inventory i = new Inventory();
			i.Items = new List<Item>();


			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Inventory WHERE Id=@id", conn);
			cmd.Parameters.AddWithValue("@Id", id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					i.Id = reader.GetInt32(reader.GetOrdinal("Id"));

					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item1id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item2id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item3id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item4id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item5id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item6id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item7id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item8id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item9id"))));
					i.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item10id"))));

					conn.Close();
					conn.Dispose();

					return i;
				}
			}
			conn.Close();
			conn.Dispose();
			return null;
		}

		public bool Update(Inventory Obj)
		{
			string query = "UPDATE dbo.Inventory SET item1id = @Slot1id, item2id = @Slot2id, item3id = @Slot3id, item4id = @Slot4id, item5id = @Slot5id, item6id = @Slot6id, item7id = @Slot7id, item8id = @Slot8id, item9id = @Slot9id, item10id = @Slot10id WHERE id = @id";

			SqlCommand com = new SqlCommand(query);

			com.Parameters.AddWithValue("id", Obj.Id);
			com.Parameters.AddWithValue("@Slot1id", Obj.Items[0].Id);
			com.Parameters.AddWithValue("@Slot2id", Obj.Items[1].Id);
			com.Parameters.AddWithValue("@Slot3id", Obj.Items[2].Id);
			com.Parameters.AddWithValue("@Slot4id", Obj.Items[3].Id);
			com.Parameters.AddWithValue("@Slot5id", Obj.Items[4].Id);
			com.Parameters.AddWithValue("@Slot6id", Obj.Items[5].Id);
			com.Parameters.AddWithValue("@Slot7id", Obj.Items[6].Id);
			com.Parameters.AddWithValue("@Slot8id", Obj.Items[7].Id);
			com.Parameters.AddWithValue("@Slot9id", Obj.Items[8].Id);
			com.Parameters.AddWithValue("@Slot10id", Obj.Items[9].Id);

			return DB.RunNonQuery(com);
		}

		public bool Insert(Inventory obj)
		{
			SqlCommand com = new SqlCommand();

			Item Emp = ItemCon.GetEmptyItem();

			string query = "INSERT INTO dbo.Inventory (item1Id, item2Id, item3Id, item4Id, item5Id, item6Id, item7Id, item8Id, item9Id, item10Id) VALUES (@slot1id, @slot2id, @slot3id, @slot4id, @slot5id, @slot6id, @slot7id, @slot8id, @slot9id, @slot10id)";

			com.CommandText = query;

			com.Parameters.AddWithValue("@Slot1id", Emp.Id);
			com.Parameters.AddWithValue("@Slot2id", Emp.Id);
			com.Parameters.AddWithValue("@Slot3id", Emp.Id);
			com.Parameters.AddWithValue("@Slot4id", Emp.Id);
			com.Parameters.AddWithValue("@Slot5id", Emp.Id);
			com.Parameters.AddWithValue("@Slot6id", Emp.Id);
			com.Parameters.AddWithValue("@Slot7id", Emp.Id);
			com.Parameters.AddWithValue("@Slot8id", Emp.Id);
			com.Parameters.AddWithValue("@Slot9id", Emp.Id);
			com.Parameters.AddWithValue("@Slot10id", Emp.Id);

			return DB.RunNonQuery(com);
		}

		public bool Remove(Inventory obj)
		{
			throw new System.NotImplementedException();
		}

		public List<Inventory> GetAll()
		{
			SqlDataReader reader = null;
			List<Inventory> result = new List<Inventory>();
			string query = "Select * from dbo.inventory";

			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
			{
				using (SqlCommand cmd = con.CreateCommand())
				{
					cmd.CommandText = query;
					try
					{
						cmd.Connection.Open();
						cmd.Prepare();
						reader = cmd.ExecuteReader();

						while (reader.Read())
						{
							Inventory obj = new Inventory();
							//obj.Slots = new List<int>();
							obj.Items = new List<Item>();
							obj.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item1id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item2id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item3id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item4id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item5id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item6id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item7id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item8id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item9id")));
							//obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item10id")));

							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item1id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item2id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item3id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item4id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item5id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item6id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item7id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item8id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item9id"))));
							obj.Items.Add(ItemCon.GetById(reader.GetInt32(reader.GetOrdinal("item10id"))));
							result.Add(obj);
						}
					}
					catch (SqlException e)
					{
						throw e;
					}
					finally
					{
						cmd.Connection.Close();
						reader?.Close();
					}
					return result;
				}
			}
		}

		public Story AddItem(Item i, Story Str)
		{
			Inventory inv = Str.Char.Inven;
			for (int a = 0; a < Str.Char.Inven.Items.Count(); a++)
			{
				if (inv.Items[a].Id == 25)// empty slot
				{
					inv.Items[a] = i;

					break; //put new item in the first empty slot and then break the loop.
				}
			}
			
			Update(inv);

			Str.Char = new Character(Str.Char, inv);
			return Str;
		}

		public Character Unequip(Item i, Character Char)
		{
			Inventory Inv = Char.Inven;
			for (int a = 0; a < Char.Inven.Items.Count(); a++)
			{
				if (Char.Inven.Items[a].Id == 25)// empty slot
				{
					Inv.Items[a] = i;

					break; //put new item in the first empty slot and then break the loop.
				}
			}
			Char = new Character(Char, Inv);
			Update(Inv);

			return Char;
		}
	}
}