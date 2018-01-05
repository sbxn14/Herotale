using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Inventory Get(int id);
		List<Inventory> GetAll();
		new bool Update(Inventory Obj);
		Story AddItem(Item i, Story Str);
		Character Unequip(Item i, Character Char);

	}
}