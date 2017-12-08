using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.ModelBinding;
using Herotale.Database;
using Herotale.IRepositories;
using Herotale.Models;
using System.Collections.Generic;
using Herotale.Contexts;

namespace Herotale.MSSQL_Repositories
{
	public class MssqlCharacterRep : ICharacterRepository
	{
		public bool Insert(Character obj)
		{
			SqlCommand com = new SqlCommand();

			Item Emp = Datamanager.GetEmptyItem();

			string query = "INSERT INTO dbo.Characters (Name, Gender, ClassId, RaceId, Health, AttackPower, Defense, Speed, CheckpointId, InventoryId, Slot1Id, Slot2Id, Slot3Id, AccountId) VALUES (@name, @gender, @Classid, @Raceid, @MaxHP, @AP, @DEF, @SPD, @Cpid, @Invenid, @Slot1id, @Slot2id, @Slot3id, @Accid)";

			com.CommandText = query;

			com.Parameters.AddWithValue("@name", obj.Name);
			com.Parameters.AddWithValue("@gender", obj.Gender);
			com.Parameters.AddWithValue("@Classid", obj.ClassId);
			com.Parameters.AddWithValue("@Raceid", obj.RaceId);
			com.Parameters.AddWithValue("@Accid", obj.AccId);
			com.Parameters.AddWithValue("@MaxHP", obj.MaxHealth);
			com.Parameters.AddWithValue("@AP", obj.AttackPower);
			com.Parameters.AddWithValue("@SPD", obj.Speed);
			com.Parameters.AddWithValue("@DEF", obj.Defense);
			com.Parameters.AddWithValue("@Invenid", obj.InvenId);
			com.Parameters.AddWithValue("@Slot1id", Emp.Id);
			com.Parameters.AddWithValue("@Slot2id", Emp.Id);
			com.Parameters.AddWithValue("@Slot3id", Emp.Id);
			com.Parameters.AddWithValue("@Cpid", 1); //Checkpoint = 1. means beginning.

			return DB.RunNonQuery(com);
		}

		public bool Remove(Character obj)
		{
			throw new System.NotImplementedException();
		}

		public Character Get(Account acc)
		{
			SqlDataReader reader = null;
			Character c = new Character();
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Characters WHERE AccountId=@id", conn);
			cmd.Parameters.AddWithValue("@id", acc.Id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					c.Id = reader.GetInt32(reader.GetOrdinal("Id"));
					c.Name = reader.GetString(reader.GetOrdinal("Name"));
					c.Gender = reader.GetString(reader.GetOrdinal("Gender"));
					c.Health = reader.GetInt32(reader.GetOrdinal("Health"));
					c.AttackPower = reader.GetInt32(reader.GetOrdinal("AttackPower"));
					c.Defense = reader.GetInt32(reader.GetOrdinal("Defense"));
					c.Speed = reader.GetInt32(reader.GetOrdinal("Speed"));

					c.ClassId = reader.GetInt32(reader.GetOrdinal("ClassId"));
					c.RaceId = reader.GetInt32(reader.GetOrdinal("RaceId"));
					c.Slot1Id = reader.GetInt32(reader.GetOrdinal("Slot1Id"));
					c.Slot2Id = reader.GetInt32(reader.GetOrdinal("Slot2Id"));
					c.Slot3Id = reader.GetInt32(reader.GetOrdinal("Slot3Id"));
					c.CpId = reader.GetInt32(reader.GetOrdinal("CheckpointId"));
					c.InvenId = reader.GetInt32(reader.GetOrdinal("InventoryId"));
					c.AccId = acc.Id;

					Datamanager.Init();

					c.Rc = Datamanager.RaceList.FirstOrDefault(x => x.Id == c.RaceId);
					c.Cl = Datamanager.ClassList.FirstOrDefault(x => x.Id == c.ClassId);
					c.Cp = Datamanager.CPList.FirstOrDefault(x => x.Id == c.CpId);
					c.Inven = Datamanager.InvenList.FirstOrDefault(x => x.Id == c.InvenId);
					c.Slot1 = Datamanager.ItemList.FirstOrDefault(x => x.Id == c.Slot1Id);
					c.Slot2 = Datamanager.ItemList.FirstOrDefault(x => x.Id == c.Slot2Id);
					c.Slot3 = Datamanager.ItemList.FirstOrDefault(x => x.Id == c.Slot3Id);
					c.Acc = Datamanager.AccList.FirstOrDefault(x => x.Id == c.AccId);
					c.MaxHealth = c.Health;
				}
				conn.Close();
				conn.Dispose();
				return c;
			}
			conn.Close();
			conn.Dispose();

			return null;
		}

		public bool CheckAcc(Account acc)
		{
			SqlDataReader reader = null;
			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Characters WHERE AccountId=@id", conn);
			cmd.Parameters.AddWithValue("@id", acc.Id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				conn.Close();
				conn.Dispose();
				return true;
			}
			conn.Close();
			conn.Dispose();

			return false;
		}

		public bool Update(Character Char)
		{
			string query = "UPDATE dbo.Characters SET AttackPower = @AP, Defense = @DEF, Speed = @SPD, Slot1Id = @Slot1Id, Slot2Id = @Slot2Id, Slot3Id = @Slot3Id, CheckpointId = @CPID WHERE Id=@Id";
			SqlCommand cmd = new SqlCommand(query);

			cmd.Parameters.AddWithValue("@AP", Char.AttackPower);
			cmd.Parameters.AddWithValue("@DEF", Char.Defense);
			cmd.Parameters.AddWithValue("@SPD", Char.Speed);
			cmd.Parameters.AddWithValue("@Slot1Id", Char.Slot1Id);
			cmd.Parameters.AddWithValue("@Slot2Id", Char.Slot2Id);
			cmd.Parameters.AddWithValue("@Slot3Id", Char.Slot3Id);
			cmd.Parameters.AddWithValue("@CPID", Char.CpId);
			cmd.Parameters.AddWithValue("@Id", Char.Id);

			return DB.RunNonQuery(cmd);
		}

		public Character Create(Character Chaa)
		{

			Datamanager.Init();
			ClassContext ClCon = new ClassContext(new MssqlClassRep());
			RaceContext RcCon = new RaceContext(new MssqlRaceRep());
			InventoryContext InCon = new InventoryContext(new MssqlInventoryRep());
			Inventory i = new Inventory();
			InCon.Insert(i);
			List<Inventory> InvenList = Datamanager.GetInventories();
			List<Account> AccList = Datamanager.AccList;
			List<Checkpoint> CpList = Datamanager.GetCps();

			Race r = RcCon.GetById(Chaa.RaceId);
			Class c = ClCon.GetById(Chaa.ClassId);
			Chaa.Cl = c;
			Chaa.Rc = r;
			i = InvenList.LastOrDefault();
			Chaa.Acc = AccList.LastOrDefault();
			Chaa.Inven = i;
			Chaa.InvenId = i.Id;
			Chaa.Cp = CpList[0];
			Chaa.CpId = Chaa.Cp.Id;
			Chaa.IsNew = true;

			switch (Chaa.ClassId)
			{
				case 1: //thief
					Chaa.AttackPower = 10 + c.AttackBonus;
					Chaa.Defense = 10 + c.DefenseBonus;
					Chaa.Speed = 10 + c.SpeedBonus;
					Chaa.Cl.Focus = 3;
					break;
				case 2: //berserker
					Chaa.AttackPower = 10 + c.AttackBonus;
					Chaa.Defense = 10 + c.DefenseBonus;
					Chaa.Speed = 10 + c.SpeedBonus;
					Chaa.Cl.Focus = 1;
					break;
				case 3: //paladin
					Chaa.AttackPower = 10 + c.AttackBonus;
					Chaa.Defense = 10 + c.DefenseBonus;
					Chaa.Speed = 10 + c.SpeedBonus;
					Chaa.Cl.Focus = 2;
					break;
			}

			switch (Chaa.RaceId)
			{
				case 1: //human
					Chaa.AttackPower = 10 + r.AttackBonus;
					Chaa.Defense = 10 + r.DefenseBonus;
					Chaa.Speed = 10 + r.SpeedBonus;
					break;
				case 2: //dwarf
					Chaa.AttackPower = 10 + r.AttackBonus;
					Chaa.Defense = 10 + r.DefenseBonus;
					Chaa.Speed = 10 + r.SpeedBonus;
					break;
				case 3: //elf
					Chaa.AttackPower = 10 + r.AttackBonus;
					Chaa.Defense = 10 + r.DefenseBonus;
					Chaa.Speed = 10 + r.SpeedBonus;
					break;
			}

			switch (Chaa.Cl.Focus)
			{
				case 1:
					{
						Chaa.MaxHealth = (100 + Chaa.AttackPower) * 2;
						break;
					}
				case 2:
					{
						Chaa.MaxHealth = (100 + Chaa.Defense) * 4;
						break;
					}
				case 3:
					{
						Chaa.MaxHealth = 100 + Chaa.Speed;
						break;
					}
			}
			Chaa.Health = Chaa.MaxHealth;

			return Chaa;
		}
	}

}