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
using Herotale.ViewModels;

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

					//c.Id = reader.GetInt32(reader.GetOrdinal("Id"));
					//c.Name = reader.GetString(reader.GetOrdinal("Name"));
					//c.Gender = reader.GetString(reader.GetOrdinal("Gender"));
					//c.Health = reader.GetInt32(reader.GetOrdinal("Health"));
					//c.AttackPower = reader.GetInt32(reader.GetOrdinal("AttackPower"));
					//c.Defense = reader.GetInt32(reader.GetOrdinal("Defense"));
					//c.Speed = reader.GetInt32(reader.GetOrdinal("Speed"));

					//c.Rc.Id = reader.GetInt32(reader.GetOrdinal("RaceId"));
					//c.Rc = rccon.GetById(c.Rc.Id);

					//c.Cl.Id = reader.GetInt32(reader.GetOrdinal("ClassId"));
					//c.Cl = clcon.GetById(c.Cl.Id);

					//c.Cp.Id = reader.GetInt32(reader.GetOrdinal("CheckpointId"));
					//c.Cp = cpcon.GetById(c.Cp.Id);

					//c.Inven.Id = reader.GetInt32(reader.GetOrdinal("InventoryId"));
					//c.Inven = invencon.GetById(c.Inven.Id);

					//c.Slot1.Id = reader.GetInt32(reader.GetOrdinal("Slot1Id"));
					//c.Slot1 = itemcon.GetById(c.Slot1.Id);

					//c.Slot2.Id = reader.GetInt32(reader.GetOrdinal("Slot2Id"));
					//c.Slot2 = itemcon.GetById(c.Slot1.Id);

					//c.Slot3.Id = reader.GetInt32(reader.GetOrdinal("Slot3Id"));
					//c.Slot3 = itemcon.GetById(c.Slot3.Id);

					//c.Acc.Id = reader.GetInt32(reader.GetOrdinal("AccountId"));
					//c.Acc = acccon.GetById(c.Acc.Id);

					//c.MaxHealth = c.Health;

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

		public bool Update(CharacterViewModel Char)
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
			Inventory i = new Inventory();
			InCon.Insert(i);
			List<Inventory> InvenList = InCon.GetAll();
			List<Account> AccList = acccon.GetAll();
			List<Checkpoint> CpList = cpcon.GetAll();

			Race r = RcCon.GetById(Chaa.Rc.Id);
			Class c = ClCon.GetById(Chaa.Cl.Id);

			i = InvenList.LastOrDefault();

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

		public bool Remove(CharacterViewModel Chaa)
		{
			throw new NotImplementedException();
		}
	}

}