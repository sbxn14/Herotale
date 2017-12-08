using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.Contexts
{
    public class CharacterContext
    {
        readonly ICharacterRepository Rep;

        public CharacterContext(ICharacterRepository rep)
        {
            Rep = rep;
        }

        public bool InsertCharacter(Character Chara)
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

        public Character Create(Character Char)
        {
            return Rep.Create(Char);
        }
    }
}