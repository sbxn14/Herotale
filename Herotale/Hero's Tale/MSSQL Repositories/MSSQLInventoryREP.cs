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
					i.Slots[0] = reader.GetInt32(reader.GetOrdinal("item1id"));
					i.Slots[1] = reader.GetInt32(reader.GetOrdinal("item2id"));
					i.Slots[2] = reader.GetInt32(reader.GetOrdinal("item3id"));
					i.Slots[3] = reader.GetInt32(reader.GetOrdinal("item4id"));
					i.Slots[4] = reader.GetInt32(reader.GetOrdinal("item5id"));
					i.Slots[5] = reader.GetInt32(reader.GetOrdinal("item6id"));
					i.Slots[6] = reader.GetInt32(reader.GetOrdinal("item7id"));
					i.Slots[7] = reader.GetInt32(reader.GetOrdinal("item8id"));
					i.Slots[8] = reader.GetInt32(reader.GetOrdinal("item9id"));
					i.Slots[9] = reader.GetInt32(reader.GetOrdinal("item10id"));

					i.Items[0] = ItemCon.GetById(i.Slots[0]);
					i.Items[1] = ItemCon.GetById(i.Slots[1]);
					i.Items[2] = ItemCon.GetById(i.Slots[2]);
					i.Items[3] = ItemCon.GetById(i.Slots[3]);
					i.Items[4] = ItemCon.GetById(i.Slots[4]);
					i.Items[5] = ItemCon.GetById(i.Slots[5]);
					i.Items[6] = ItemCon.GetById(i.Slots[6]);
					i.Items[7] = ItemCon.GetById(i.Slots[7]);
					i.Items[8] = ItemCon.GetById(i.Slots[8]);
					i.Items[9] = ItemCon.GetById(i.Slots[9]);

					conn.Close();
					conn.Dispose();

					return i;
				}
			}
			conn.Close();
			conn.Dispose();
			return null;
		}

		public bool Update(Inventory obj)
		{
			throw new System.NotImplementedException();
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
			ItemContext itemcon = new ItemContext(new MssqlItemRep());
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
							obj.Slots = new List<int>();
							obj.Items = new List<Item>();
							obj.Id = reader.GetInt32(reader.GetOrdinal("Id"));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item1id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item2id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item3id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item4id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item5id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item6id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item7id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item8id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item9id")));
							obj.Slots.Add(reader.GetInt32(reader.GetOrdinal("item10id")));

							obj.Items.Add(itemcon.GetById(obj.Slots[0]));
							obj.Items.Add(itemcon.GetById(obj.Slots[1]));
							obj.Items.Add(itemcon.GetById(obj.Slots[2]));
							obj.Items.Add(itemcon.GetById(obj.Slots[3]));
							obj.Items.Add(itemcon.GetById(obj.Slots[4]));
							obj.Items.Add(itemcon.GetById(obj.Slots[5]));
							obj.Items.Add(itemcon.GetById(obj.Slots[6]));
							obj.Items.Add(itemcon.GetById(obj.Slots[7]));
							obj.Items.Add(itemcon.GetById(obj.Slots[8]));
							obj.Items.Add(itemcon.GetById(obj.Slots[9]));
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
	}
}