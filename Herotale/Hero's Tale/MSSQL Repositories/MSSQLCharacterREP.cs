using Herotale.Contexts;
using Herotale.Database;
using Herotale.IRepositories;
using Herotale.Models;
using Herotale.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Herotale.MSSQL_Repositories
{
	public class MssqlCharacterRep : ICharacterRepository
	{
		public bool Insert(CharacterViewModel obj)
		{
			SqlCommand com = new SqlCommand();
			ItemContext itemcot = new ItemContext(new MssqlItemRep());
			Item Emp = itemcot.GetEmptyItem();

			string query = "INSERT INTO dbo.Characters (Name, Gender, ClassId, RaceId, Health, AttackPower, Defense, Speed, CheckpointId, InventoryId, Slot1Id, Slot2Id, Slot3Id, AccountId) VALUES (@name, @gender, @Classid, @Raceid, @MaxHP, @AP, @DEF, @SPD, @Cpid, @Invenid, @Slot1id, @Slot2id, @Slot3id, @Accid)";

			com.CommandText = query;

			com.Parameters.AddWithValue("@name", obj.Name);
			com.Parameters.AddWithValue("@gender", obj.Gender);
			com.Parameters.AddWithValue("@Classid", obj.Cl.Id);
			com.Parameters.AddWithValue("@Raceid", obj.Rc.Id);
			com.Parameters.AddWithValue("@Accid", obj.Acc.Id);
			com.Parameters.AddWithValue("@MaxHP", obj.MaxHealth);
			com.Parameters.AddWithValue("@AP", obj.AttackPower);
			com.Parameters.AddWithValue("@SPD", obj.Speed);
			com.Parameters.AddWithValue("@DEF", obj.Defense);
			com.Parameters.AddWithValue("@Invenid", obj.Inven.Id);
			com.Parameters.AddWithValue("@Slot1id", Emp.Id);
			com.Parameters.AddWithValue("@Slot2id", Emp.Id);
			com.Parameters.AddWithValue("@Slot3id", Emp.Id);
			com.Parameters.AddWithValue("@Cpid", 7); //Checkpoint = 7. means beginning.

			return DB.RunNonQuery(com);
		}

		public bool Remove(Character obj)
		{
			throw new System.NotImplementedException();
		}

		public Character Get(Account acc)
		{
			SqlDataReader reader = null;
			RaceContext rccon = new RaceContext(new MssqlRaceRep());
			ClassContext clcon = new ClassContext(new MssqlClassRep());
			CheckpointContext cpcon = new CheckpointContext(new MSSQLCheckpointREP());
			InventoryContext invencon = new InventoryContext(new MssqlInventoryRep());
			ItemContext itemcon = new ItemContext(new MssqlItemRep());
			AccountContext acccon = new AccountContext(new MssqlAccountRep());
			Character ca = new Character();


			SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
			conn.Open();
			SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Characters WHERE AccountId=@id", conn);
			cmd.Parameters.AddWithValue("@id", acc.Id);
			reader = cmd.ExecuteReader();

			if (reader != null && reader.HasRows)
			{
				while (reader.Read())
				{
					int rcid = reader.GetInt32(reader.GetOrdinal("RaceId"));
					int clid = reader.GetInt32(reader.GetOrdinal("ClassId"));
					int cpid = reader.GetInt32(reader.GetOrdinal("CheckpointId"));
					int invenid = reader.GetInt32(reader.GetOrdinal("InventoryId"));
					int slot1id = reader.GetInt32(reader.GetOrdinal("Slot1Id"));
					int slot2id = reader.GetInt32(reader.GetOrdinal("Slot2Id"));
					int slot3id = reader.GetInt32(reader.GetOrdinal("Slot3Id"));

					int accid = acc.Id;
					ca = new Character(reader.GetInt32(reader.GetOrdinal("Id")), reader.GetString(reader.GetOrdinal("Name")), reader.GetString(reader.GetOrdinal("Gender")), reader.GetInt32(reader.GetOrdinal("Health")), reader.GetInt32(reader.GetOrdinal("Health")), reader.GetInt32(reader.GetOrdinal("AttackPower")), reader.GetInt32(reader.GetOrdinal("Defense")), reader.GetInt32(reader.GetOrdinal("Speed")), cpcon.GetById(cpid), clcon.GetById(clid), rccon.GetById(rcid), invencon.GetById(invenid), acccon.GetById(accid), itemcon.GetById(slot1id), itemcon.GetById(slot2id), itemcon.GetById(slot3id));
				}
				conn.Close();
				conn.Dispose();
				return ca;
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
			cmd.Parameters.AddWithValue("@Slot1Id", Char.Slot1.Id);
			cmd.Parameters.AddWithValue("@Slot2Id", Char.Slot2.Id);
			cmd.Parameters.AddWithValue("@Slot3Id", Char.Slot3.Id);
			cmd.Parameters.AddWithValue("@CPID", Char.Cp.Id);
			cmd.Parameters.AddWithValue("@Id", Char.Id);

			return DB.RunNonQuery(cmd);
		}

		public CharacterViewModel Create(CharacterViewModel Chaa)
		{
			ClassContext ClCon = new ClassContext(new MssqlClassRep());
			RaceContext RcCon = new RaceContext(new MssqlRaceRep());
			InventoryContext InCon = new InventoryContext(new MssqlInventoryRep());
			AccountContext acccon = new AccountContext(new MssqlAccountRep());
			CheckpointContext cpcon = new CheckpointContext(new MSSQLCheckpointREP());
			ItemContext iCon = new ItemContext(new MssqlItemRep());
			Inventory i = new Inventory();
			Item it = iCon.GetEmptyItem();
			InCon.Insert(i);
			List<Inventory> InvenList = InCon.GetAll();
			List<Account> AccList = acccon.GetAll();
			List<Checkpoint> CpList = cpcon.GetAll();

			Race r = RcCon.GetById(Chaa.Rc.Id);
			Class c = ClCon.GetById(Chaa.Cl.Id);

			i = InvenList.LastOrDefault();

			Chaa.Slot1 = it;
			Chaa.Slot2 = it;
			Chaa.Slot3 = it;

			Chaa.Acc = acccon.GetById(Chaa.Acc.Id);
			Chaa.Inven = i;
			Chaa.Cp = CpList[0];

			switch (Chaa.Cl.Id)
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

			switch (Chaa.Rc.Id)
			{
				case 1: //human
					Chaa.AttackPower += r.AttackBonus;
					Chaa.Defense += r.DefenseBonus;
					Chaa.Speed += r.SpeedBonus;
					break;
				case 2: //dwarf
					Chaa.AttackPower += r.AttackBonus;
					Chaa.Defense += r.DefenseBonus;
					Chaa.Speed += r.SpeedBonus;
					break;
				case 3: //elf
					Chaa.AttackPower += r.AttackBonus;
					Chaa.Defense += r.DefenseBonus;
					Chaa.Speed += r.SpeedBonus;
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

		public bool Insert(Character obj)
		{
			throw new NotImplementedException();
		}

		public Character EquipItem(int Whatitem, Character Cha)
		{
			ItemContext ItemCon = new ItemContext(new MssqlItemRep());
			Item i = Cha.Inven.Items[Whatitem - 1];
			int id = 0;
			Item Empty = ItemCon.GetEmptyItem();

			if (Cha.Slot1.Id != 25)
			{
				id = 1;

				Cha.Inven.Items[Whatitem - 1] = Empty;
				Cha = new Character(Cha, i, id);
			}
			else if (Cha.Slot2.Id != 25)
			{
				id = 2;

				Cha.Inven.Items[Whatitem - 1] = Empty;
				Cha = new Character(Cha, i, id);
			}
			else if (Cha.Slot3.Id != 25)
			{
				id = 3;

				Cha.Inven.Items[Whatitem - 1] = Empty;
				Cha = new Character(Cha, i, id);
			}
			else
			{

			}
			return Cha;
		}

		public Character DequipItem(int WhatSlot, Character Cha)
		{
			ItemContext ItemCon = new ItemContext(new MssqlItemRep());
			InventoryContext InvenCon = new InventoryContext(new MssqlInventoryRep());
			Item i = new Item();
			Item Empty = ItemCon.GetEmptyItem();

			if (WhatSlot == 1)
			{
				i = Cha.Slot1;

			}
			else if (WhatSlot == 2)
			{
				i = Cha.Slot2;
			}
			else if (WhatSlot == 3)
			{
				i = Cha.Slot3;
			}
			Cha = new Character(Cha, Empty, WhatSlot);
			Cha = InvenCon.Dequip(i, Cha);

			return Cha;
		}
	}
}