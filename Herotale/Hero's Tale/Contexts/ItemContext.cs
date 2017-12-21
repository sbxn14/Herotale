using Herotale.IRepositories;
using Herotale.Models;
using System.Collections.Generic;

namespace Herotale.Contexts
{
	public class ItemContext
    {
		readonly IItemRepository Rep;

		public ItemContext(IItemRepository repo)
		{
			Rep = repo;
		}

		public Item GetEmptyItem()
		{
			return Rep.GetEmptyItem();
		}

		public Item GetById(int id)
		{
			return Rep.Get(id);
		}

		public List<Item> GetAll()
		{
			return Rep.GetAll();
		}

		public Item GetRandomItem()
		{
			return Rep.GetRandomItem();
		}
	}
}