using Herotale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
		public int CpId { get; set; }
		public Checkpoint Cp { get; set; }
		public Class Cl { get; set; }
		public Race Rc { get; set; }
		public Inventory Inven { get; set; }
		public Account Acc { get; set; }
		public Item Slot1 { get; set; }
		public Item Slot2 { get; set; }
		public Item Slot3 { get; set; }
	}
}