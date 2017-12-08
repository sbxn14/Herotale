using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Inventory Get(int id);
    }
}