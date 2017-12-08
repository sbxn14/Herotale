using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface ICharacterRepository : IRepository<Character>
    {
        Character Get(Account acc);
        Character Create(Character Chaa);
        bool CheckAcc(Account acc);
    }
}