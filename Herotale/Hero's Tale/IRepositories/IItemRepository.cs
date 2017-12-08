using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IItemRepository : IRepository<Item>
    {
        Item Get();
    }
}