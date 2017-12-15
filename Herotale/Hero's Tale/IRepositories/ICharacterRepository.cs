using System.Collections.Generic;
using Herotale.Models;
using Herotale.ViewModels;

namespace Herotale.IRepositories
{
	public interface ICharacterRepository : IRepository<CharacterViewModel>
	{
		Character Get(Account acc);
		CharacterViewModel Create(CharacterViewModel Chaa);
		bool CheckAcc(Account acc);
	}
}