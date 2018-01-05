using Herotale.Models;
using System.ComponentModel;

namespace Herotale.ViewModels
{
	public class CharacterViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Gender { get; set; }
		public int Health { get; set; }
		public int MaxHealth { get; set; }
		public int AttackPower { get; set; }
		public int Defense { get; set; }
		public int Speed { get; set; }
		public Checkpoint Cp { get; set; }
		[DisplayName("Class")]
		public Class Cl { get; set; }
		[DisplayName("Race")]
		public Race Rc { get; set; }
		public Inventory Inven { get; set; }
		public Account Acc { get; set; }
		public Item Slot1 { get; set; }
		public Item Slot2 { get; set; }
		public Item Slot3 { get; set; }

		public CharacterViewModel()
		{

		}

		public CharacterViewModel(Account a)
		{
			Acc = a;
		}

		public CharacterViewModel(Character Cha)
		{
			Id = Cha.Id;
			Name = Cha.Name;
			Gender = Cha.Gender;
			Health = Cha.Health;
			MaxHealth = Health;
			AttackPower = Cha.AttackPower;
			Defense = Cha.Defense;
			Speed = Cha.Speed;
			Cp = Cha.Cp;
			Rc = Cha.Rc;
			Inven = Cha.Inven;
			Acc = Cha.Acc;
			Slot1 = Cha.Slot1;
			Slot2 = Cha.Slot2;
			Slot3 = Cha.Slot3;
		}
	}
}