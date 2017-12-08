using Herotale.Contexts;
using Herotale.Database;
using Herotale.MSSQL_Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;

namespace Herotale.Models
{
    public class Character : IQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int CpId { get; set; }
        public Checkpoint Cp { get; set; }
        public Class Cl { get; set; }
        [Display(Name = "Class")]
        public int ClassId { get; set; }
        [Display(Name = "Race")]
        public int RaceId { get; set; }
        public Race Rc { get; set; }
        public int InvenId { get; set; }
        public Inventory Inven { get; set; }
        public int AccId { get; set; }
        public Account Acc { get; set; }
        public int Slot1Id { get; set; }
        public int Slot2Id { get; set; }
        public int Slot3Id { get; set; }
        public Item Slot1 { get; set; }
        public Item Slot2 { get; set; }
        public Item Slot3 { get; set; }
        public bool IsNew { get; set; }
        public string Query { get; set; }

        public Character()
        {
            Query = "SELECT * FROM dbo.Characters";
        }

        public void Parse(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id"));
            Name = reader.GetString(reader.GetOrdinal("Name"));
            Gender = reader.GetString(reader.GetOrdinal("Gender"));
            Health = reader.GetInt32(reader.GetOrdinal("Health"));
            AttackPower = reader.GetInt32(reader.GetOrdinal("AttackPower"));
            Defense = reader.GetInt32(reader.GetOrdinal("Defense"));
            Speed = reader.GetInt32(reader.GetOrdinal("Speed"));
            AccId = reader.GetInt32(reader.GetOrdinal("AccountId"));

            ClassId = reader.GetInt32(reader.GetOrdinal("ClassId"));
            RaceId = reader.GetInt32(reader.GetOrdinal("RaceId"));
            Slot1Id = reader.GetInt32(reader.GetOrdinal("Slot1Id"));
            Slot2Id = reader.GetInt32(reader.GetOrdinal("Slot2Id"));
            Slot3Id = reader.GetInt32(reader.GetOrdinal("Slot3Id"));
            CpId = reader.GetInt32(reader.GetOrdinal("CheckpointId"));

            Rc = Datamanager.RaceList.FirstOrDefault(x => x.Id == RaceId);
            Cl = Datamanager.ClassList.FirstOrDefault(x => x.Id == ClassId);
            Cp = Datamanager.CPList.FirstOrDefault(x => x.Id == CpId);
            Inven = Datamanager.InvenList.FirstOrDefault(x => x.Id == InvenId);
            Slot1 = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slot1Id);
            Slot2 = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slot2Id);
            Slot3 = Datamanager.ItemList.FirstOrDefault(x => x.Id == Slot3Id);
            Acc = Datamanager.AccList.FirstOrDefault(x => x.Id == AccId);
        }
    }
}