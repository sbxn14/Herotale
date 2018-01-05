using Herotale.IRepositories;
using Herotale.Models;
using Herotale.ViewModels;

namespace Herotale.Contexts
{
	public class CharacterContext
    {
        readonly ICharacterRepository Rep;

        public CharacterContext(ICharacterRepository rep)
        {
            Rep = rep;
        }

        public bool InsertCharacter(CharacterViewModel Chara)
        {
			return Rep.Insert(Chara);
        }

        public bool CheckForCharacter(Account acc)
        {
            return Rep.CheckAcc(acc);
        }

        public Character Get(Account acc)
        {
            return Rep.Get(acc);
        }

        public bool Update(Character Char)
        {
            return Rep.Update(Char);
        }

        public CharacterViewModel Calculate(CharacterViewModel Char)
        {
            return Rep.Create(Char);
        }
		
		public Character EquipItem(int Whatitem, Character Cha)
		{
			return Rep.EquipItem(Whatitem, Cha);
		}

		public Character UnequipItem(int WhatSlot, Character Cha)
		{
			return Rep.UnequipItem(WhatSlot, Cha);
		}

		public bool Remove(Character obj)
		{
			return Rep.Remove(obj);
		}
	}
}