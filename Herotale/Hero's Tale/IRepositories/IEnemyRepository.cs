using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IEnemyRepository : IRepository<Enemy>
    {
        Enemy Get(int id);
		List<Enemy> GetAll();
		Enemy GetByName(string name);
    }
}