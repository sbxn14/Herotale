using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Inventory Get(int id);
		List<Inventory> GetAll();
		bool Update(Character Obj);
		Story AddItem(Item i, Story Str);
		Character Dequip(Item i, Character Char);

	}
}