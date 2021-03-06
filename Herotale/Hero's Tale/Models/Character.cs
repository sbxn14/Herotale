﻿using Herotale.ViewModels;

namespace Herotale.Models
{
	public class Character
	{
		public int Id { get; }
		public string Name { get; }
		public string Gender { get; }
		public int Health { get; }
		public int MaxHealth { get; }
		public int AttackPower { get; }
		public int Defense { get; }
		public int Speed { get; }
		public Checkpoint Cp { get; }
		public Class Cl { get; }
		public Race Rc { get; }
		public Inventory Inven { get; }
		public Account Acc { get; }
		public Item Slot1 { get; }
		public Item Slot2 { get; }
		public Item Slot3 { get; }
		public int OldProgress { get; set; }
		public int TemporaryItem { get; set; }

		// for ingame
		public Character(CharacterViewModel c)
		{
			Id = c.Id;
			Name = c.Name;
			Gender = c.Gender;
			Health = c.Health;
			MaxHealth = c.MaxHealth;
			AttackPower = c.AttackPower;
			Defense = c.Defense;
			Speed = c.Speed;
			Cp = c.Cp;
			Cl = c.Cl;
			Rc = c.Rc;
			Inven = c.Inven;
			Acc = c.Acc;
			Slot1 = c.Slot1;
			Slot2 = c.Slot2;
			Slot3 = c.Slot3;
		}

		//for database
		public Character(int id, string name, string gender, int health, int maxhealth, int attackpower, int defense, int speed, Checkpoint cp, Class cl, Race rc, Inventory inven, Account acc, Item slot1, Item slot2, Item slot3)
		{
			Id = id;
			Name = name;
			Gender = gender;
			Health = health;
			MaxHealth = Health;
			AttackPower = attackpower;
			Defense = defense;
			Speed = speed;
			Cp = cp;
			Cl = cl;
			Rc = rc;
			Inven = inven;
			Acc = acc;
			Slot1 = slot1;
			Slot2 = slot2;
			Slot3 = slot3;
		}

		public Character(Account a)
		{
			Acc = a;
		}

		public Character()
		{
			MaxHealth = Health;
		}

		//public Character(Character c, int health) //for combat damage
		//{
		//	Id = c.Id;
		//	Name = c.Name;
		//	Gender = c.Gender;
		//	Health = health;
		//	MaxHealth = c.MaxHealth;
		//	AttackPower = c.AttackPower;
		//	Defense = c.Defense;
		//	Speed = c.Speed;
		//	Cp = c.Cp;
		//	Cl = c.Cl;
		//	Rc = c.Rc;
		//	Inven = c.Inven;
		//	Acc = c.Acc;
		//	Slot1 = c.Slot1;
		//	Slot2 = c.Slot2;
		//	Slot3 = c.Slot3;
		//}

		public Character(Character c, int AP, int DEF, int SPD)
		{
			Id = c.Id;
			Name = c.Name;
			Gender = c.Gender;
			Health = c.Health;
			MaxHealth = c.MaxHealth;
			AttackPower = AP;
			Defense = DEF;
			Speed = SPD;
			Cp = c.Cp;
			Cl = c.Cl;
			Rc = c.Rc;
			Inven = c.Inven;
			Acc = c.Acc;
			Slot1 = c.Slot1;
			Slot2 = c.Slot2;
			Slot3 = c.Slot3;
		}

		public Character(Checkpoint CPP, Character c)
		{
			Cp = CPP;
			Id = c.Id;
			Name = c.Name;
			Gender = c.Gender;
			Health = c.Health;
			MaxHealth = c.MaxHealth;
			AttackPower = c.AttackPower;
			Defense = c.Defense;
			Speed = c.Speed;
			Cl = c.Cl;
			Rc = c.Rc;
			Inven = c.Inven;
			Acc = c.Acc;
			Slot1 = c.Slot1;
			Slot2 = c.Slot2;
			Slot3 = c.Slot3;
		}

		public Character(Character c, Inventory Inv)
		{
			Cp = c.Cp;
			Id = c.Id;
			Name = c.Name;
			Gender = c.Gender;
			Health = c.Health;
			MaxHealth = c.MaxHealth;
			AttackPower = c.AttackPower;
			Defense = c.Defense;
			Speed = c.Speed;
			Cl = c.Cl;
			Rc = c.Rc;
			Inven = Inv;
			Acc = c.Acc;
			Slot1 = c.Slot1;
			Slot2 = c.Slot2;
			Slot3 = c.Slot3;
		}

		public Character(Character c, Item i, int id, Inventory Inv) //for equipping/Unequipping
		{
			Cp = c.Cp;
			Id = c.Id;
			Name = c.Name;
			Gender = c.Gender;
			Health = c.Health;
			MaxHealth = c.MaxHealth;
			AttackPower = c.AttackPower;
			Defense = c.Defense;
			Speed = c.Speed;
			Cl = c.Cl;
			Rc = c.Rc;
			Inven = Inv;
			Acc = c.Acc;
			Slot1 = i;
			Slot2 = c.Slot2;
			Slot3 = c.Slot3;

			if (id == 1)
			{
				Slot1 = i;
			}
			else if (id == 2)
			{
				Slot2 = i;
			}
			else if (id == 3)
			{
				Slot3 = i;
			}
			else
			{
				Slot1 = c.Slot1;
				Slot2 = c.Slot2;
				Slot3 = c.Slot3;
			}
		}
		public Character(Character c, Item i, int id) //for equipping/unequipping
		{
			Cp = c.Cp;
			Id = c.Id;
			Name = c.Name;
			Gender = c.Gender;
			Health = c.Health;
			MaxHealth = c.MaxHealth;
			AttackPower = c.AttackPower;
			Defense = c.Defense;
			Speed = c.Speed;
			Cl = c.Cl;
			Rc = c.Rc;
			Inven = c.Inven;
			Acc = c.Acc;
			Slot1 = i;
			Slot2 = c.Slot2;
			Slot3 = c.Slot3;

			if (id == 1)
			{
				Slot1 = i;
			}
			else if (id == 2)
			{
				Slot2 = i;
			}
			else if (id == 3)
			{
				Slot3 = i;
			}
			else
			{
				Slot1 = c.Slot1;
				Slot2 = c.Slot2;
				Slot3 = c.Slot3;
			}
		}
	}
}