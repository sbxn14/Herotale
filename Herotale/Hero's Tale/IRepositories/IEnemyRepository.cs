using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IEnemyRepository : IRepository<Enemy>
    {
        Enemy Get(int id);
		int Randomizer();
		List<Enemy> GetAll();
		Enemy RandomMob(int randomnumber, List<Enemy> Enemies);
    }
}