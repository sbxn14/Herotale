using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IRaceRepository : IRepository<Race>
    {
        Race Get(int id);
    }
}