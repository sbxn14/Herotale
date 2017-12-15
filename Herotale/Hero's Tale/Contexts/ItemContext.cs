using Herotale.IRepositories;
using Herotale.Models;

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
	}
}