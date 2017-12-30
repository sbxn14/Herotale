using System.Collections.Generic;
using Herotale.Models;
using Herotale.ViewModels;

namespace Herotale.IRepositories
{
	public interface ICharacterRepository : IRepository<Character>
	{
		Character Get(Account acc);
		CharacterViewModel Create(CharacterViewModel Chaa);
		bool CheckAcc(Account acc);
		bool Insert(CharacterViewModel obj);
		Character EquipItem(int WhatItem, Character Cha);
		Character DequipItem(int WhatSlot, Character Cha);
	}
}