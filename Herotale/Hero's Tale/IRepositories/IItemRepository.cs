using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Item Get(int id);
		Item GetEmptyItem();
		List<Item> GetAll();
		Item GetRandomItem();
    }
}